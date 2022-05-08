using System.Collections;

namespace Essenbee.Mix
{
    public class MixWord
    {
        private enum SignEnum
        {
            Positive,
            Negative,
        };

        private SignEnum Sign { get; set; } = SignEnum.Positive;
        private BitArray Fields = new BitArray(30);

        public static int MAX_VALUE = 1_073_741_823;

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
                    throw new ArgumentOutOfRangeException("Value is outside the valid range!");
                }

                Fields = new BitArray(new int[] { _value });
            }
        }

        public int this[byte i]
        {
            get
            {
                return this[i, i];
            }
        }

        public int this[byte l, byte r]
        {
            get
            {
                if (Fields is null)
                {
                    throw new InvalidDataException("Fields property is null!");
                }

                SignEnum sign = Sign;

                if ((l == 0) && (r == 0))
                {
                    return (Sign == SignEnum.Negative) ? (-1) : 1;
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

        public string ToOpString() => $"[{(Sign == SignEnum.Positive ? "+|" : "-|")}{string.Format("{0:D2}", this[1, 2])}|I={string.Format("{0:D2}", this[3])}|F={string.Format("{0:D2}", this[4])}|Op={string.Format("{0:D2}", this[5])}]";

        private int ReadFieldSpec(byte l, byte r)
        {
            int startIndex = 30 - (6 * r);
            int endIndex = 29 - ((l - 1) * 6);
            int length = endIndex - startIndex;
            BitArray subField = new BitArray(new int[] { 0 });
            int j = 0;

            for (int i = startIndex; i < endIndex + 1; i++)
            {
                subField[j++] = Fields[i];
            }

            int[] result = new int[1];
            subField.CopyTo(result, 0);
            return result[0];
        }

        public override string ToString() => $"[{(Sign == SignEnum.Positive ? "+|" : "-|")}{string.Format("{0:D2}", this[1])}|{string.Format("{0:D2}", this[2])}|{string.Format("{0:D2}", this[3])}|{string.Format("{0:D2}", this[4])}|{string.Format("{0:D2}", this[5])}]";

        public static MixWord operator -(MixWord word) => new MixWord(-word.Value);
        public static MixWord operator ++(MixWord word) => new MixWord(word.Value + 1);
        public static MixWord operator --(MixWord word) => new MixWord(word.Value - 1);
        public static MixWord operator +(MixWord word1, MixWord word2) => new MixWord(word1.Value + word2.Value);
        public static MixWord operator -(MixWord word1, MixWord word2) => new MixWord(word1.Value - word2.Value);
    }
}