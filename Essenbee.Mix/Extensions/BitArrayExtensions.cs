using System.Collections;

namespace Essenbee.Mix.Extensions
{
    public static class BitArrayExtensions
    {
        public static BitArray Append(this BitArray current, BitArray after)
        {
            var ints = new int[(current.Count + after.Count) / 32];
            current.CopyTo(ints, 0);
            after.CopyTo(ints, current.Count / 32);
            return new BitArray(ints);
        }
    }
}
