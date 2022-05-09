using Essenbee.Mix.Enums;
using System.Collections;

namespace Essenbee.Mix
{
    // MixWord is non-nullable
    public class MixWord : IEquatable<MixWord?>
    {
        public SignEnum Sign { get; set; } = SignEnum.Positive;
        private BitArray Fields = new(30);

        public const int MAX_VALUE = 1_073_741_823;
        public const int BYTE_SIZE = 6;
        public const int ADDRESS_SIZE = 12;

        private int _value;
        public int Value
        {
            get => (Sign == SignEnum.Negative) ? (-_value) : _value;
            set
            {
                if (value < 0)
                {
                    Sign = SignEnum.Negative;
                }

                _value = Math.Abs(value);

                if (_value > MAX_VALUE)
                {
                    throw new ArgumentOutOfRangeException($"Invalid value {(Sign == SignEnum.Negative ? -value : value)}");
                }

                Fields = new BitArray(new int[] { _value });
            }
        }

        public int this[byte i]
        {
            get => this[i, i];
        }

        public int Read(FieldSpec spec)
        {
             return this[spec[0], spec[1]];
        }

        public int GetFieldSpec()
        {
            return this[4];
        }

        public int GetAddress()
        {
            return this[0, 2];
        }

        public int GetIndexer()
        {
            var indexer = this[3];
            return indexer;
        }

        public int GetOpCode()
        {
            var indexer = this[5];
            return indexer;
        }

        public void Write (int data, FieldSpec spec)
        {
            if (spec is null)
            {
                throw new InvalidDataException("A valid FieldSpec must be provided - use the default if it is not required");
            }

            if (spec.Length == 1)
            {
                Sign = (data < 0) ? SignEnum.Negative : SignEnum.Positive;
                return;
            }

            if (spec[0] == 0 && spec.Length == 30)
            {
                Value = data;
            }
            else if (spec[0] == 1 && spec.Length == 30)
            {
                var priorSign = Sign;
                Value = data;
                Sign = priorSign;
            }
            else 
            {
                var left = spec[0];
                var priorSign = Sign;

                if (left == 0)
                {
                    left++;
                    Sign = (data < 0) ? SignEnum.Negative : SignEnum.Positive;
                }

                var startPos = (30 - spec.Length) - ( 6 * (left - 1));
                var dataBits = new BitArray(new int[] { Math.Abs(data) });
                var j = 0;

                for (int i = startPos; i < (startPos + spec.Length); i++)
                {
                    Fields.Set(i, dataBits[j++]);
                }
            }
        }

        public void SetInstructionWord(byte opCode, short address, FieldSpec spec, byte indexRegister = 0)
        {
            if (address < -3_999 || address > 3_999)
            {
                throw new ArgumentOutOfRangeException($"Address must be in the range -3999 to 3999 (was {address})");
            }

            if (indexRegister < 0 || indexRegister > 6)
            {
                throw new ArgumentOutOfRangeException($"Index Register must be in the range 0 - 6 (was {indexRegister})");
            }

            if (opCode < 0 || opCode > 63)
            {
                throw new ArgumentOutOfRangeException($"Op Code must be in the range 0 - 63 (was {opCode})");
            }

            if (spec is null)
            {
                throw new InvalidDataException("A valid FieldSpec must be provided - use the default if it is not required");
            }

            Fields = new BitArray(30);
            Sign = address < 0 ? SignEnum.Negative : SignEnum.Positive;

            var addressBits = new BitArray(new int[] { Math.Abs(address) });
            var indexBits = new BitArray(new int[] { Math.Abs(indexRegister) });
            var fieldSpecBits = new BitArray(new int[] { Math.Abs(FieldSpec.Value(spec)) });
            var opBits = new BitArray(new int[] { Math.Abs(opCode) });

            MapToFields(addressBits, indexBits, fieldSpecBits, opBits);
        }

        public int this[byte l, byte r]
        {
            get
            {
                if (Fields is null)
                {
                    throw new InvalidDataException("Fields property is null!");
                }

                FieldSpec.ThrowIfArgsInvalid(l, r);

                if ((l == 0) && (r == 0))
                {
                    return (Sign == SignEnum.Negative) ? (-1) : 1;
                }

                if ((l == 0) && (r == 6))
                {
                    // ToDo: coded decimal
                    throw new NotImplementedException("Coded decimal handling is an unimplemented feature");
                }

                if (l == 0 && r == 5)
                {
                    return (Sign == SignEnum.Negative) ? (-_value) : _value;
                }

                if (l == 0)
                {
                    l++;
                    int retVal = ReadFieldSpec(l, r);
                    return (Sign == SignEnum.Negative) ? (-retVal) : retVal;
                }
                else
                {
                    return ReadFieldSpec(l, r);
                }
            }
        }

        public MixWord()
        {
            Value = 0;
        }

        public MixWord(int value)
        {
            Value = value;
        }

        public MixWord(MixWord word)
        {
            Value = word.Value;
        }

        public bool Equals(MixWord? other) => other is not null && Value == other.Value;
        public string ToOpString() => $"[{(Sign == SignEnum.Positive ? "+|" : "-|")}{string.Format("{0:D2}", this[1, 2])}|I={string.Format("{0:D2}", this[3, 3])}|F={FieldSpec.Instance((byte)this[4, 4])}|Op={string.Format("{0:D2}", this[5, 5])}]";

        private int ReadFieldSpec(byte l, byte r)
        {
            int startIndex = 30 - (6 * r);
            int endIndex = 29 - ((l - 1) * 6);
            BitArray subField = new(new int[] { 0 });
            int j = 0;

            for (int i = startIndex; i < endIndex + 1; i++)
            {
                subField[j++] = Fields[i];
            }

            int[] result = new int[1];
            subField.CopyTo(result, 0);
            return result[0];
        }

        private void MapToFields(BitArray addressBits, BitArray indexBits, BitArray fieldSpecBits, BitArray opBits)
        {
            for (int i = 0; i < BYTE_SIZE; i++)
            {
                Fields.Set(i, opBits[i]);
            }

            for (int i = 0; i < BYTE_SIZE; i++)
            {
                Fields.Set(i + BYTE_SIZE, fieldSpecBits[i]);
            }

            for (int i = 0; i < BYTE_SIZE; i++)
            {
                Fields.Set(i + (2 * BYTE_SIZE), indexBits[i]);
            }

            for (int i = 0; i < ADDRESS_SIZE; i++)
            {
                Fields.Set(i + (3 * BYTE_SIZE), addressBits[i]);
            }
        }

        // Overrides and Overloads
        public override string ToString() => $"[{(Sign == SignEnum.Positive ? "+|" : "-|")}{string.Format("{0:D2}", this[1])}|{string.Format("{0:D2}", this[2])}|{string.Format("{0:D2}", this[3])}|{string.Format("{0:D2}", this[4])}|{string.Format("{0:D2}", this[5])}]";
        public override bool Equals(object? obj) => Equals(obj as MixWord);
        public override int GetHashCode() => HashCode.Combine(Value);
        public static MixWord operator -(MixWord word) => new(-word.Value);
        public static MixWord operator ++(MixWord word) => new(word.Value + 1);
        public static MixWord operator --(MixWord word) => new(word.Value - 1);
        public static MixWord operator +(MixWord word1, MixWord word2) => new(word1.Value + word2.Value);
        public static MixWord operator -(MixWord word1, MixWord word2) => new(word1.Value - word2.Value);
        public static bool operator ==(MixWord word1, MixWord word2) => word1.Equals(word2);
        public static bool operator !=(MixWord word1, MixWord word2) => !word1.Equals(word2);
        public static bool operator <(MixWord word1, MixWord word2) => word1.Value < word2.Value;
        public static bool operator >(MixWord word1, MixWord word2) => word1.Value > word2.Value;
        public static bool operator <=(MixWord word1, MixWord word2) => word1.Value <= word2.Value;
        public static bool operator >=(MixWord word1, MixWord word2) => word1.Value >= word2.Value;
        public static implicit operator int(MixWord word) => word.Value;
        public static explicit operator MixWord(int i) => new(i);
    }
}