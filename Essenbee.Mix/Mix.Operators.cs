using Essenbee.Mix.Enums;

namespace Essenbee.Mix
{
    public partial class Mix
    {
        private void NOP(MixAddress addr, byte i, byte f)
        {
            PC++;
        }

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

        private void LD1(MixAddress addr, byte i, byte f)
        {
            LoadIReg(0, addr, i, f);
            PC++;
        }

        private void LD2(MixAddress addr, byte i, byte f)
        {
            LoadIReg(1, addr, i, f);
            PC++;
        }

        private void LD3(MixAddress addr, byte i, byte f)
        {
            LoadIReg(2, addr, i, f);
            PC++;
        }

        private void LD4(MixAddress addr, byte i, byte f)
        {
            LoadIReg(3, addr, i, f);
            PC++;
        }

        private void LD5(MixAddress addr, byte i, byte f)
        {
            LoadIReg(4, addr, i, f);
            PC++;
        }

        private void LD6(MixAddress addr, byte i, byte f)
        {
            LoadIReg(5, addr, i, f);
            PC++;
        }

        private void LoadIReg(int n, MixAddress addr, byte i, byte f)
        {
            var spec = FieldSpec.Instance(f);

            if (spec.Length > 12)
            {
                throw new InvalidDataException($"Operation is undefined for FieldSpec of {spec}");
            }

            var loc = addr + (i > 0 ? I[i - 1].Value : 0);

            if (f == 0)
            {
                I[n].Sign = RAM[loc].Sign;
            }
            else
            {
                var val = RAM[loc].Read(FieldSpec.Instance(f));
                I[n] =new MixAddress(val);
            }
        }

        private void LDAN(MixAddress addr, byte i, byte f)
        {
            var loc = addr + (i > 0 ? I[i - 1].Value : 0);

            if (f == 0)
            {
                A.Sign = (RAM[loc].Sign == SignEnum.Negative) ? SignEnum.Positive : SignEnum.Negative;
            }
            else
            {
                A.Load(RAM[loc], FieldSpec.Instance(f));
                A.Sign = (RAM[loc].Sign == SignEnum.Negative) ? SignEnum.Positive : SignEnum.Negative;
            }

            PC++;
        }

        private void LDXN(MixAddress addr, byte i, byte f)
        {
            var loc = addr + (i > 0 ? I[i - 1].Value : 0);

            if (f == 0)
            {
                X.Sign = (RAM[loc].Sign == SignEnum.Negative) ? SignEnum.Positive : SignEnum.Negative;
            }
            else
            {
                X.Load(RAM[loc], FieldSpec.Instance(f));
                X.Sign = (RAM[loc].Sign == SignEnum.Negative) ? SignEnum.Positive : SignEnum.Negative;
            }

            PC++;
        }
    }
}
