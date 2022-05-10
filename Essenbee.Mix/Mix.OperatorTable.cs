namespace Essenbee.Mix
{
    public partial class Mix
    {
        private Dictionary<byte, MixOperator> LoadMixOperators()
        {
            return new Dictionary<byte, MixOperator>
            {
                 { 8, new MixOperator("LDA", LDA, 1) },

                 { 15, new MixOperator("LDX", LDX, 1) },
            };
        }
    }
}
