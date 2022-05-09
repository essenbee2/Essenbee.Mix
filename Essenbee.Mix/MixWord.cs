using System.Collections;

namespace Essenbee.Mix
{
    // MixWord is non-nullable
    public class MixWord : IEquatable<MixWord?>
    {
        private enum SignEnum
        {
            Positive,
            Negative,
        };

        private SignEnum Sign { get; set; } = SignEnum.Positive;
        private BitArray Fields = new(30);

        public const int MAX_VALUE = 1_073_741_823;

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

        public int FromFieldSpec(byte i)
        {
                var r = (byte)(i / 8);
                var l = (byte)(i % 8);
                return this[l, r];
        }

        public int this[byte l, byte r]
        {
            get
            {
                if (Fields is null)
                {
                    throw new InvalidDataException("Fields property is null!");
                }

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
        public string ToOpString() => $"[{(Sign == SignEnum.Positive ? "+|" : "-|")}{string.Format("{0:D2}", this[1, 2])}|I={string.Format("{0:D2}", this[3, 3])}|F={string.Format("{0:D2}", this[4, 4])}|Op={string.Format("{0:D2}", this[5, 5])}]";

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