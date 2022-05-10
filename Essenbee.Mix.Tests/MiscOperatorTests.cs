using Xunit;

namespace Essenbee.Mix.Tests
{
    public class MiscOperatorTests
    {
        [Fact]
        public void HLT()
        {
            var mix = new Mix();

            mix.RAM[0].Write(0, 2000, FieldSpec.Default); // NOP
            mix.RAM[1].Write(0, 2000, FieldSpec.Default); // NOP
            mix.RAM[2].Write(0, 2000, FieldSpec.Default); // NOP
            mix.RAM[3].Write(0, 2000, FieldSpec.Default); // NOP
            mix.RAM[4].Write(0, 2000, FieldSpec.Default); // NOP
            mix.RAM[5].Write(5, 2000, FieldSpec.Default); // HLT

            mix.Run();

            Assert.Equal(6, mix.PC);
            Assert.Equal(6, mix.ExecutionTime);
        }
    }
}
