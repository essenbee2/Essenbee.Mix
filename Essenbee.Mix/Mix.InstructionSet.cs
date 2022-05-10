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
                { 9, new MixOperator("LD1", LD1, 2) },
                { 10, new MixOperator("LD2", LD2, 2) },
                { 11, new MixOperator("LD3", LD3, 2) },
                { 12, new MixOperator("LD4", LD4, 2) },
                { 13, new MixOperator("LD5", LD5, 2) },
                { 14, new MixOperator("LD6", LD6, 2) },
                { 15, new MixOperator("LDX", LDX, 2) },
                { 16, new MixOperator("LDAN", LDAN, 2) },

                { 23, new MixOperator("LDXN", LDXN, 2) },
            };
        }
    }
}
