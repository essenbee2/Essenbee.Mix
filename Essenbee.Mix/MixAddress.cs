using Essenbee.Mix.Enums;
using System.Collections;

namespace Essenbee.Mix
{
    public class MixAddress
    {
        public SignEnum Sign { get; set; } = SignEnum.Positive;
        private BitArray Fields = new(12);

        public const int MAX_VALUE = 3_999;

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
                    throw new ArgumentOutOfRangeException($"Invalid address {(Sign == SignEnum.Negative ? -value : value)}");
                }

                Fields = new BitArray(new int[] { _value });
            }
        }

        private int this[byte l, byte r]
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

                if (l == 0 && r == 2)
                {
                    return (Sign == SignEnum.Negative) ? (-_value) : _value;
                }

                if (l == 0)
                {
                    l++;
                    int retVal = ReadSixBitBlocks(l, r);
                    return (Sign == SignEnum.Negative) ? (-retVal) : retVal;
                }
                else
                {
                    return ReadSixBitBlocks(l, r);
                }
            }
        }

        private int ReadSixBitBlocks(byte l, byte r)
        {
            int startIndex = 12 - (6 * r);
            int endIndex = 11 - ((l - 1) * 6);
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

        public MixAddress()
        {
            Value = 0;
        }

        public MixAddress(int value)
        {
            Value = value;
        }

        public byte HiByte() => (byte)this[1, 1];
        public byte LoByte() => (byte)this[2, 2];
        public bool Equals(MixAddress? other) => other is not null && Value == other.Value;

        // Overrides and Overloads
        public override string ToString() => $"[{(Sign == SignEnum.Positive ? "+|" : "-|")}{string.Format("{0:D4}", Value)}]";
        public override bool Equals(object? obj) => Equals(obj as MixAddress);
        public override int GetHashCode() => HashCode.Combine(Value);
        public static MixAddress operator -(MixAddress address) => new(-address.Value);
        public static MixAddress operator ++(MixAddress address) => new(address.Value + 1);
        public static MixAddress operator --(MixAddress address) => new(address.Value - 1);
        public static MixAddress operator +(MixAddress address1, MixAddress address2) => new(address1.Value + address2.Value);
        public static MixAddress operator -(MixAddress address1, MixAddress address2) => new(address1.Value - address2.Value);
        public static bool operator ==(MixAddress address1, MixAddress address2) => address1.Equals(address2);
        public static bool operator !=(MixAddress address1, MixAddress address2) => !address1.Equals(address2);
        public static bool operator <(MixAddress address1, MixAddress address2) => address1.Value < address2.Value;
        public static bool operator >(MixAddress address1, MixAddress address2) => address1.Value > address2.Value;
        public static bool operator <=(MixAddress address1, MixAddress address2) => address1.Value <= address2.Value;
        public static bool operator >=(MixAddress address1, MixAddress address2) => address1.Value >= address2.Value;
        public static implicit operator int(MixAddress address) => address.Value;
        public static explicit operator MixAddress(int i) => new(i);
    }
}
