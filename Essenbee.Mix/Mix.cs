namespace Essenbee.Mix
{
    public partial class Mix
    {
        public enum Comparison
        {
            LESS,
            EQUAL,
            GREATER,
        };

        // Registers
        public MixWord A { get; set; } = new(0);
        public MixWord X { get; set; } = new(0);
        public MixAddress[] I { get; set; } = new MixAddress[6];
        public int PC { get; set; } = 0;
        public MixWord J { get; set; } = new(0);

        // Flags
        public Comparison C { get; set; } = Comparison.EQUAL;
        public bool OV { get; set; } = false; // Overflow flag

        public MixWord[] RAM = new MixWord[4000];

        public Dictionary<byte, MixOperator> MixOperators = new();

        public int ExecutionTime { get; set; } = 0;

        public Mix()
        {
            Initialise();
            MixOperators = LoadMixOperators();
        }

        public void Reset()
        {
            Initialise();
        }

        private void Initialise()
        {
            for (var i = 0; i < 4000; i++)
            {
                RAM[i] = new MixWord(0);
            }

            PC = 0;

            A = new MixWord(0);
            X = new MixWord(0);
            J = new MixWord(0);

            I = new MixAddress[6];

            for (var i = 0; i < 6; i++)
            {
                I[i] = new MixAddress(0);
            }

            OV = false;
            C = Comparison.EQUAL;

            ExecutionTime = 0;
        }

        public void Step()
        {
            var (opCode, addr, i, f) = RAM[PC].ReadOperator();
            MixOperators[opCode].Exec(addr, i, f);
        }
    }
}
