using System;
using Xunit;

namespace Essenbee.Mix.Tests
{
    public class A_MixAddress_Must
    {
        [Fact]
        public void Represent_Valid_Addresses_Correctly()
        {
            var address1 = new MixAddress(1_877); // [+|011101010101]
            var loByte = address1.LoByte();
            var hiByte = address1.HiByte();

            var address2 = new MixAddress(-1_877); // [-|011101010101]

            Assert.Equal(1_877, address1.Value);
            Assert.Equal(Enums.SignEnum.Positive, address1.Sign);
            Assert.Equal(Enums.SignEnum.Negative, address2.Sign);
            Assert.Equal(21, loByte);
            Assert.Equal(29, hiByte);
            Assert.Throws<ArgumentOutOfRangeException>(() => new MixAddress(4_001));
            Assert.Throws<ArgumentOutOfRangeException>(() => new MixAddress(-4_000));
        }

        [Fact]
        public void ToString_Correctly()
        {
            var address = new MixAddress(1_877); // [+|011101010101]

            Assert.Equal("[+|1877]", address.ToString());
        }

        [Fact]
        public void Increment_Correctly()
        {
            var address = new MixAddress(1_877); // [+|011101010101]
            var newAddress = ++address;
            var address2 = new MixAddress(3_999);

            Assert.Equal(1_878, newAddress.Value);
            Assert.Throws<ArgumentOutOfRangeException>(() => ++address2);
        }

        [Fact]
        public void Decrement_Correctly()
        {
            var address = new MixAddress(1_877); // [+|011101010101]
            var newAddress = --address;
            var address2 = new MixAddress(-3_999);

            Assert.Equal(1_876, newAddress.Value);
            Assert.Throws<ArgumentOutOfRangeException>(() => --address2);
        }

        [Fact]
        public void Add_Correctly()
        {
            var address1 = new MixAddress(7);
            var address2 = new MixAddress(3);
            var address3 = new MixAddress(2);
            address3 += new MixAddress(2);
            var address4 = new MixAddress(3_995);

            Assert.Equal(10, (address1 + address2).Value);
            Assert.Equal(14, (address1 + address1).Value);
            Assert.Equal(4, address3.Value);
            Assert.Throws<ArgumentOutOfRangeException>(() => address4 + address1 );
        }

        [Fact]
        public void Subtract_Correctly()
        {
            var address1 = new MixAddress(7);
            var address2 = new MixAddress(3);
            var address3 = new MixAddress(25);
            address3 -= new MixAddress(3);
            var address4 = new MixAddress(-3_995);

            Assert.Equal(4, (address1 - address2).Value);
            Assert.Equal(0, (address1 - address1).Value);
            Assert.Equal(22, address3.Value);
            Assert.Throws<ArgumentOutOfRangeException>(() => address4 - address1);
        }

        [Fact]
        public void Negate_Correctly()
        {
            var address1 = new MixAddress(7);
            var address2 = -address1;
            var address3 = -address2;

            Assert.Equal(-7, address2.Value);
            Assert.Equal(7, address3.Value);
        }

        [Fact]
        public void Handle_Equality_Correctly()
        {
            var address1 = new MixAddress(7);
            var address2 = new MixAddress(7);
            var address3 = -address2;

            Assert.True(address1 == address2);
            Assert.False(address2 == address3);
            Assert.True(address1.Equals(address2));
            Assert.False(address2.Equals(address3));
        }

        [Fact]
        public void Handle_Comparisons_Correctly()
        {
            var address1 = new MixAddress(7);
            var address2 = new MixAddress(10);

            Assert.True(address1 < address2);
            Assert.True(address1 <= address2);
            Assert.True(address2 > address1);
            Assert.True(address2 >= address1);
            Assert.False(address1 > address2);
            Assert.False(address1 >= address2);
            Assert.False(address2 < address1);
            Assert.False(address2 <= address1);
        }

        [Fact]
        public void Handle_Type_Conversions_Correctly()
        {
            var address1 = new MixAddress(7);
            int aValue = address1;
            int bValue = (int)address1;

            int cValue = 42;
            var word2 = (MixAddress)cValue;

            Assert.Equal(7, aValue);
            Assert.Equal(7, bValue);
            Assert.Equal(42, word2.Value);
        }
    }
}
