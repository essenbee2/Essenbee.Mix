using System;
using Xunit;

namespace Essenbee.Mix.Tests
{
    public class A_FieldSpec_Must
    {
        [Fact]
        public void Handle_Equality()
        {
            var spec1 = FieldSpec.Instance(1, 5);
            var spec2 = FieldSpec.Instance(41);
            var spec3 = FieldSpec.Default;
            var spec4 = FieldSpec.Instance(0, 5);

            Assert.Equal(spec1, spec2);
            Assert.Equal(spec3, spec4);
        }

        [Fact]
        public void Throw_For_Invalid_Inputs()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => FieldSpec.Instance(0, 7));
            Assert.Throws<ArgumentOutOfRangeException>(() => FieldSpec.Instance(6, 5));
            Assert.Throws<ArgumentOutOfRangeException>(() => FieldSpec.Instance(7));
            Assert.Throws<ArgumentOutOfRangeException>(() => FieldSpec.Instance(63));
            Assert.Throws<ArgumentOutOfRangeException>(() => FieldSpec.Instance(2, 0)); // Wrong order!
        }

        [Fact]
        public void Have_Correct_Length()
        {
            var spec = FieldSpec.Instance(0, 5);
            Assert.Equal(30, spec.Length);
            spec = FieldSpec.Instance(1, 5);
            Assert.Equal(30, spec.Length);
            spec = FieldSpec.Instance(2, 5);
            Assert.Equal(24, spec.Length);
            spec = FieldSpec.Instance(3, 5);
            Assert.Equal(18, spec.Length);
            spec = FieldSpec.Instance(4, 5);
            Assert.Equal(12, spec.Length);
            spec = FieldSpec.Instance(5, 5);
            Assert.Equal(6, spec.Length);
            spec = FieldSpec.Instance(4, 4);
            Assert.Equal(6, spec.Length);;
            spec = FieldSpec.Instance(3, 3);
            Assert.Equal(6, spec.Length);
            spec = FieldSpec.Instance(2, 2);
            Assert.Equal(6, spec.Length);;
            spec = FieldSpec.Instance(1, 1);
            Assert.Equal(6, spec.Length);
            spec = FieldSpec.Instance(0, 0);
            Assert.Equal(1, spec.Length); // Sign Bit
        }
    }
}
