using System;
using Xunit;

namespace Essenbee.Mix.Tests
{
    public class A_FieldSpec_Must
    {
        [Fact]
        public void Handle_Equality()
        {
            var spec1 = FieldSpec.Get(1, 5);
            var spec2 = FieldSpec.Get(41);

            Assert.Equal(spec1, spec2);
        }

        [Fact]
        public void Throw_For_Invalid_Inputs()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => FieldSpec.Get(0, 7));
            Assert.Throws<ArgumentOutOfRangeException>(() => FieldSpec.Get(6, 5));
            Assert.Throws<ArgumentOutOfRangeException>(() => FieldSpec.Get(7));
            Assert.Throws<ArgumentOutOfRangeException>(() => FieldSpec.Get(63));
            Assert.Throws<ArgumentOutOfRangeException>(() => FieldSpec.Get(2, 0)); // Wrong order!
        }
    }
}
