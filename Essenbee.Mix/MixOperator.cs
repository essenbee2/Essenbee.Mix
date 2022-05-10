namespace Essenbee.Mix
{
    public class MixOperator
    {
        public string Mnemonic { get; set; }
        public Action<MixAddress, byte, byte> Op { get; set; }
        public ushort Timing { get; set; } = 0;

        public MixOperator(string mnemonic, Action<MixAddress, byte, byte> op, ushort timing)
        {
            Mnemonic = mnemonic;
            Op = op;
            Timing = timing;
        }

        public void Exec(MixAddress addr, byte i, byte f)
        {
            Op(addr, i, f);
        }
    }
}
