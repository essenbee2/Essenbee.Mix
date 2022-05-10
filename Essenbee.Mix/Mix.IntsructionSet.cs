namespace Essenbee.Mix
{
    public partial class Mix
    {
        private Dictionary<byte, MixOperator> LoadInstructionSet()
        {
            return new Dictionary<byte, MixOperator>
            {
                { 0, new MixOperator("NOP", NOP, 1) },
                { 5, new MixOperator("HLT", NOP, 1) },

                { 8, new MixOperator("LDA", LDA, 2) },

                { 15, new MixOperator("LDX", LDX, 2) },
            };
        }
    }
}
