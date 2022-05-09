namespace Essenbee.Mix
{
    public class FieldSpec : Tuple<byte, byte>, IEquatable<FieldSpec?>
    {
        private FieldSpec(byte left, byte right) : base(left, right)
        {
            var posLeft = left;
            var posRight = right;

            if (posLeft != 0 && posLeft == posRight)
            {
                Length = 6;
            }
            else if (posLeft == posRight)
            {
                Length = 1;
            }
            else
            { 
                if (posLeft == 0)
                {
                    posLeft++;
                }

                Length = ((posRight - posLeft) + 1) * 6;
            }
        }

        public static FieldSpec Default { get => new(0, 5); }
        public int Length { get; set; }
        public static int Value(FieldSpec fieldSpec)
        {
            return (fieldSpec[1] * 8) + fieldSpec[0];
        }
        public static FieldSpec Instance(byte spec)
        {
            var right = (byte)(spec / 8);
            var left = (byte)(spec % 8);

            ThrowIfArgsInvalid(left, right);
            return new FieldSpec(left, right);
        }

        public static FieldSpec Instance(byte left, byte right)
        {
            ThrowIfArgsInvalid(left, right);
            return new FieldSpec(left, right);
        }

        public byte this[int index]
        {
            get
            {
                return index switch
                {
                    0 => Item1,
                    1 => Item2,
                    _ => throw new ArgumentOutOfRangeException(nameof(index)),
                };
            }
        }

        public bool Equals(FieldSpec? other)
        {
            return other != null &&
                   base.Equals(other) &&
                   Item1 == other.Item1 &&
                   Item2 == other.Item2;
        }

        public override bool Equals(object? obj) => Equals(obj as FieldSpec);
        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), Item1, Item2);
        public static bool operator ==(FieldSpec? left, FieldSpec? right) => EqualityComparer<FieldSpec>.Default.Equals(left, right);
        public static bool operator !=(FieldSpec? left, FieldSpec? right) => !(left == right);
        public override string ToString() => $"({this[0]}:{this[1]})";

        public static void ThrowIfArgsInvalid(byte left, byte right)
        {
            if (left < 0 || left > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(left));
            }

            if (right < 0 || right > 6)
            {
                throw new ArgumentOutOfRangeException(nameof(right));
            }

            if (left > right)
            {
                throw new ArgumentOutOfRangeException(nameof(left));
            }
        }
    }
}
