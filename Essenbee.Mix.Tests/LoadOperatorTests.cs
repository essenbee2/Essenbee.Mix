using Essenbee.Mix.Enums;
using System;
using Xunit;

namespace Essenbee.Mix.Tests
{
    public class LoadOperatorTests
    {
        [Fact]
        public void LDA()
        {
            var mix = new Mix();
            // Contents of memory cell 2000
            mix.RAM[2000].Write(-80, FieldSpec.Instance(0, 2));
            mix.RAM[2000].Write(3, FieldSpec.Instance(3, 3));
            mix.RAM[2000].Write(5, FieldSpec.Instance(4, 4));
            mix.RAM[2000].Write(4, FieldSpec.Instance(5, 5));

            mix.RAM[2005].Write(21, FieldSpec.Instance(0, 2));
            mix.RAM[2005].Write(0, FieldSpec.Instance(3, 3));
            mix.RAM[2005].Write(16, FieldSpec.Instance(4, 4));
            mix.RAM[2005].Write(9, FieldSpec.Instance(5, 5));

            mix.RAM[0].Write(8, 2000, FieldSpec.Default);
            _ = mix.Step();

            Assert.Equal(mix.RAM[2000].Value, mix.A);
            Assert.True(mix.A.Sign == SignEnum.Negative);

            mix.PC = 0;
            mix.A = new MixWord(0);
            mix.RAM[0].Write(8, 2000, FieldSpec.Instance(1, 5));
            _ = mix.Step();

            Assert.Equal(Math.Abs(mix.RAM[2000].Value), mix.A.Value);
            Assert.True(mix.A.Sign == SignEnum.Positive);

            mix.PC = 0;
            mix.A = new MixWord(0);
            mix.RAM[0].Write(8, 2000, FieldSpec.Instance(3, 5));
            _ = mix.Step();

            Assert.Equal(3, mix.A[3]);
            Assert.Equal(5, mix.A[4]);
            Assert.Equal(4, mix.A[5]);
            Assert.True(mix.A.Sign == SignEnum.Positive);

            mix.PC = 0;
            mix.A = new MixWord(0);
            mix.RAM[0].Write(8, 2000, FieldSpec.Instance(0, 3));
            _ = mix.Step();

            Assert.Equal(80, mix.A[3, 4]);
            Assert.Equal(3, mix.A[5]);
            Assert.True(mix.A.Sign == SignEnum.Negative);

            mix.PC = 0;
            mix.A = new MixWord(0);
            mix.RAM[0].Write(8, 2000, FieldSpec.Instance(4, 4));
            _ = mix.Step();

            mix.PC = 0;
            mix.A = new MixWord(0);
            mix.RAM[0].Write(8, 2000, FieldSpec.Instance(1, 1));
            _ = mix.Step();

            mix.PC = 0;
            mix.A = new MixWord(0);
            mix.RAM[0].Write(8, 2000, FieldSpec.Instance(0, 0));
            _ = mix.Step();

            Assert.Equal(0, mix.A.Value);
            Assert.True(mix.A.Sign == SignEnum.Negative);

            // Indexed by I registers
            mix.PC = 0;
            mix.A = new MixWord(0);
            mix.I[0] = new MixAddress(5);  //I1
            mix.RAM[0].Write(8, 2000, FieldSpec.Default, 1);
            _ = mix.Step();

            Assert.Equal(mix.RAM[2005].Value, mix.A);
            Assert.True(mix.A.Sign == SignEnum.Positive);

            mix.PC = 0;
            mix.A = new MixWord(0);
            mix.I[2] = new MixAddress(-5);  //I2
            mix.RAM[0].Write(8, 2005, FieldSpec.Default, 3);
            _ = mix.Step();

            Assert.Equal(mix.RAM[2000].Value, mix.A);
            Assert.True(mix.A.Sign == SignEnum.Negative);
        }
        [Fact]
        public void LDX()
        {
            var mix = new Mix();
            // Contents of memory cell 2000
            mix.RAM[2000].Write(-80, FieldSpec.Instance(0, 2));
            mix.RAM[2000].Write(3, FieldSpec.Instance(3, 3));
            mix.RAM[2000].Write(5, FieldSpec.Instance(4, 4));
            mix.RAM[2000].Write(4, FieldSpec.Instance(5, 5));

            mix.RAM[2005].Write(21, FieldSpec.Instance(0, 2));
            mix.RAM[2005].Write(0, FieldSpec.Instance(3, 3));
            mix.RAM[2005].Write(16, FieldSpec.Instance(4, 4));
            mix.RAM[2005].Write(9, FieldSpec.Instance(5, 5));

            mix.RAM[0].Write(15, 2000, FieldSpec.Default);
            _ = mix.Step();

            Assert.Equal(mix.RAM[2000].Value, mix.X);
            Assert.True(mix.X.Sign == SignEnum.Negative);

            mix.PC = 0;
            mix.X = new MixWord(0);
            mix.RAM[0].Write(15, 2000, FieldSpec.Instance(1, 5));
            _ = mix.Step();

            Assert.Equal(Math.Abs(mix.RAM[2000].Value), mix.X.Value);
            Assert.True(mix.X.Sign == SignEnum.Positive);

            mix.PC = 0;
            mix.X = new MixWord(0);
            mix.RAM[0].Write(15, 2000, FieldSpec.Instance(3, 5));
            _ = mix.Step();

            Assert.Equal(3, mix.X[3]);
            Assert.Equal(5, mix.X[4]);
            Assert.Equal(4, mix.X[5]);
            Assert.True(mix.X.Sign == SignEnum.Positive);

            mix.PC = 0;
            mix.X = new MixWord(0);
            mix.RAM[0].Write(15, 2000, FieldSpec.Instance(0, 3));
            _ = mix.Step();

            Assert.Equal(80, mix.X[3, 4]);
            Assert.Equal(3, mix.X[5]);
            Assert.True(mix.X.Sign == SignEnum.Negative);

            mix.PC = 0;
            mix.X = new MixWord(0);
            mix.RAM[0].Write(15, 2000, FieldSpec.Instance(4, 4));
            _ = mix.Step();

            mix.PC = 0;
            mix.X = new MixWord(0);
            mix.RAM[0].Write(15, 2000, FieldSpec.Instance(1, 1));
            mix.Step();

            mix.PC = 0;
            mix.X = new MixWord(0);
            mix.RAM[0].Write(15, 2000, FieldSpec.Instance(0, 0));
            _ = mix.Step();

            Assert.Equal(0, mix.X.Value);
            Assert.True(mix.X.Sign == SignEnum.Negative);

            // Indexed by I registers
            mix.PC = 0;
            mix.X = new MixWord(0);
            mix.I[0] = new MixAddress(5);  //I1
            mix.RAM[0].Write(15, 2000, FieldSpec.Default, 1);
            _ = mix.Step();

            Assert.Equal(mix.RAM[2005].Value, mix.X);
            Assert.True(mix.X.Sign == SignEnum.Positive);

            mix.PC = 0;
            mix.X = new MixWord(0);
            mix.I[2] = new MixAddress(-5);  //I2
            mix.RAM[0].Write(15, 2005, FieldSpec.Default, 3);
            _ = mix.Step();

            Assert.Equal(mix.RAM[2000].Value, mix.X);
            Assert.True(mix.X.Sign == SignEnum.Negative);
        }
    }
}
