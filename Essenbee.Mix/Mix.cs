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

        public static ushort MEMSIZE = 4000;

        // Registers
        public MixWord A { get; set; } = new(0); // Accumulator
        public MixWord X { get; set; } = new(0); // Extension Register
        public MixAddress[] I { get; set; } = new MixAddress[6]; // Hold address offsets for indexing
        public int PC { get; set; } = 0; // Program Counter
        public MixWord J { get; set; } = new(0); // Jump Register

        // Flags
        public Comparison C { get; set; } = Comparison.EQUAL;
        public bool OV { get; set; } = false; // Overflow flag

        // Memory
        public MixWord[] RAM = new MixWord[MEMSIZE];

        // Instruction Set
        public Dictionary<byte, MixOperator> InstructionSet = new();

        public int ExecutionTime { get; set; } = 0;

        public Mix()
        {
            Initialise();
            InstructionSet = LoadInstructionSet();
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

        public void Run()
        {
            while(true)
            {
                var lastOp = Step();

                if (lastOp.Equals(5) || PC >= MEMSIZE )
                {
                    // HLT or PC exceeded memory
                    break;
                }
            }
        }

        public byte Step()
        {
            var (opCode, addr, i, f) = RAM[PC].ReadOperator();
            InstructionSet[opCode].Exec(addr, i, f);
            ExecutionTime += InstructionSet[opCode].Timing;
            return opCode;
        }
    }
}
