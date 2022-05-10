using Essenbee.Mix.Enums;

namespace Essenbee.Mix
{
    public partial class Mix
    {
        // ==========================
        // Load Operators
        // ==========================

        private void LDA(MixAddress addr, byte i, byte f)
        {
            var loc = addr + (i > 0 ? I[i - 1].Value : 0);

            if (f == 0)
            {
                A.Sign = RAM[loc].Sign;
            }
            else
            {
                A.Load(RAM[loc], FieldSpec.Instance(f));
            }

            PC++;
        }

        private void LDX(MixAddress addr, byte i, byte f)
        {
            var loc = addr + (i > 0 ? I[i - 1].Value : 0);

            if (f == 0)
            {
                X.Sign = RAM[loc].Sign;
            }
            else
            {
                X.Load(RAM[loc], FieldSpec.Instance(f));
            }

            PC++;
        }
    }
}
