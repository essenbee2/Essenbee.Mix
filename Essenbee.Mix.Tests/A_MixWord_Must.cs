using System;
using Xunit;

namespace Essenbee.Mix.Tests
{
    public class A_MixWord_Must
    {
        [Fact]
        public void Represent_Valid_Values_Correctly()
        {
            var word = new MixWord(-357_913_941); // [-|010101|010101|010101|010101|010101]

            // Field access by indices...
            var val00 = word[0, 0]; // -1
            var val05 = word[0, 5]; // -357_913_941
            var val02 = word[0, 2]; // -1_365
            var val15 = word[1, 5]; // 357_913_941
            var val25 = word[2, 5]; // 5_592_405
            var val35 = word[3, 5]; // 87_381
            var val44 = word[4, 4]; // 21
            var val45 = word[4, 5]; // 1_365

            Assert.Equal(-357_913_941, word.Value);
            Assert.Equal(Enums.SignEnum.Negative, word.Sign);
            Assert.Equal(-1, val00);
            Assert.Equal(-357_913_941, val05);
            Assert.Equal(357_913_941, val15);
            Assert.Equal(-1_365, val02);
            Assert.Equal(5_592_405, val25);
            Assert.Equal(87_381, val35);
            Assert.Equal(21, val44);
            Assert.Equal(1_365, val45);

            // Throw exception if indices are out of range...
            Assert.Throws<ArgumentOutOfRangeException>(() => word[2, 1]); // Wrong order!
            Assert.Throws<ArgumentOutOfRangeException>(() => word[0, 7]); // Right value is too big
        }

        [Fact]
        public void Get_Values_By_FieldSpec_Correctly()
        {
            var word = new MixWord(-357_913_941); // [-|010101|010101|010101|010101|010101]

            var val00 = word.FromFieldSpec(FieldSpec.Get(0, 0)); // -1
            var val05 = word.FromFieldSpec(FieldSpec.Get(0, 5)); // -357_913_941
            var val02 = word.FromFieldSpec(FieldSpec.Get(0, 2)); // -1_365
            var val15 = word.FromFieldSpec(FieldSpec.Get(1, 5)); // 357_913_941
            var val25 = word.FromFieldSpec(FieldSpec.Get(2, 5)); // 5_592_405
            var val35 = word.FromFieldSpec(FieldSpec.Get(3, 5)); // 87_381
            var val44 = word.FromFieldSpec(FieldSpec.Get(4, 4)); // 21
            var val45 = word.FromFieldSpec(FieldSpec.Get(4, 5)); // 1_365

            Assert.Equal(-357_913_941, word.Value);
            Assert.Equal(-1, val00);
            Assert.Equal(-357_913_941, val05);
            Assert.Equal(357_913_941, val15);
            Assert.Equal(-1_365, val02);
            Assert.Equal(5_592_405, val25);
            Assert.Equal(87_381, val35);
            Assert.Equal(21, val44);
            Assert.Equal(1_365, val45);
        }

        [Fact]
        public void Get_Instruction_Sections_Correctly()
        {
            var word = new MixWord(-357_831_816); // [-|010101|010101|000001|010010|001000]

            var address = word.GetAddress(); // -1_365
            var indexRegister = word.GetIndexer(); // 1
            var fieldSpec = word.GetFieldSpec(); // 18
            var op = word.GetOpCode(); // 8

            Assert.Equal(-1_365, address);
            Assert.Equal(1, indexRegister);
            Assert.Equal(18, fieldSpec);
            Assert.Equal(8, op);
        }

        [Fact]
        public void ToString_Correctly()
        {
            var word = new MixWord(-357_913_941); // [-|010101|010101|010101|010101|010101]

            Assert.Equal("[-|21|21|21|21|21]", word.ToString());
        }

        [Fact]
        public void ToOpString_Correctly()
        {
            var word = new MixWord(-357_827_925); // [-|010101|010101|000000|010101|010101]

            Assert.Equal("[-|1365|I=00|F=21|Op=21]", word.ToOpString());
        }

        [Fact]
        public void Increment_Correctly()
        {
            var word = new MixWord(-357_913_941); // -|010101|010101|010101|010101|010101
            var newWord = ++word;

            Assert.Equal(-357_913_940, newWord.Value);
        }

        [Fact]
        public void Decrement_Correctly()
        {
            var word = new MixWord(-357_913_941); // -|010101|010101|010101|010101|010101
            var newWord = --word;

            Assert.Equal(-357_913_942, newWord.Value);
        }

        [Fact]
        public void Add_Correctly()
        {
            var word1 = new MixWord(7);
            var word2 = new MixWord(3);
            var word3 = new MixWord(2);
            word3 += new MixWord(2);

            Assert.Equal(10, (word1 + word2).Value);
            Assert.Equal(14, (word1 + word1).Value);
            Assert.Equal(4, word3.Value);
        }

        [Fact]
        public void Subtract_Correctly()
        {
            var word1 = new MixWord(7);
            var word2 = new MixWord(3);
            var word3 = new MixWord(25);
            word3 -= new MixWord(3);

            Assert.Equal(4, (word1 - word2).Value);
            Assert.Equal(0, (word1 - word1).Value);
            Assert.Equal(22, word3.Value);
        }

        [Fact]
        public void Negate_Correctly()
        {
            var word1 = new MixWord(7);
            var word2 = -word1;
            var word3 = -word2;

            Assert.Equal(-7, word2.Value);
            Assert.Equal(7, word3.Value);
        }

        [Fact]
        public void Handle_Equality_Correctly()
        {
            var word1 = new MixWord(7);
            var word2 = new MixWord(7);
            var word3 = -word2;

            Assert.True(word1 == word2);
            Assert.False(word2 == word3);
            Assert.True(word1.Equals(word2));
            Assert.False(word2.Equals(word3));
        }

        [Fact]
        public void Handle_Comparisons_Correctly()
        {
            var word1 = new MixWord(7);
            var word2 = new MixWord(10);

            Assert.True(word1 < word2);
            Assert.True(word1 <= word2);
            Assert.True(word2 > word1);
            Assert.True(word2 >= word1);
            Assert.False(word1 > word2);
            Assert.False(word1 >= word2);
            Assert.False(word2 < word1);
            Assert.False(word2<= word1);
        }

        [Fact]
        public void Handle_Type_Conversions_Correctly()
        {
            var word1 = new MixWord(7);
            int aValue = word1;
            int bValue = (int)word1;

            int cValue = 42;
            var word2 = (MixWord)cValue;

            var word3 = new MixWord(99_856); // [+|000000|000000|011000|011000|010000]
            int dvalue = word3[4, 5]; // Using a field spec
            int evalue = word3[2, 2]; // Using a field spec

            Assert.Equal(7, aValue);
            Assert.Equal(7, bValue);
            Assert.Equal(42, word2.Value);
            Assert.Equal(1_552, dvalue);
            Assert.Equal(0, evalue);
        }
    }
}