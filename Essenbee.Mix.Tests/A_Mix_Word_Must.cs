using Xunit;

namespace Essenbee.Mix.Tests
{
    public class A_Mix_Word_Must
    {
        [Fact]
        public void Represent_Valid_Values_Correctly()
        {
            var word = new MixWord(-357_913_941); // [-|010101|010101|010101|010101|010101]

            var val00 = word[0, 0]; // -1
            var val05 = word[0, 5]; // -357_913_941
            var val02 = word[0, 2]; // -1_365
            var val15 = word[1, 5]; // 357_913_941
            var val25 = word[2, 5]; // 5_592_405
            var val35 = word[3, 5]; // 87_381
            var val44 = word[4, 4]; // 21
            var val45 = word[4, 5]; // 1_365

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
        public void ToString_Values_Correctly()
        {
            var word = new MixWord(-357_913_941); // [-|010101|010101|010101|010101|010101]

            Assert.Equal("[-|21|21|21|21|21]", word.ToString());
        }

        [Fact]
        public void Increment_Values_Correctly()
        {
            var word = new MixWord(-357_913_941); // -|010101|010101|010101|010101|010101
            var newWord = ++word;

            Assert.Equal(-357_913_940, newWord.Value);
        }

        [Fact]
        public void Decrement_Values_Correctly()
        {
            var word = new MixWord(-357_913_941); // -|010101|010101|010101|010101|010101
            var newWord = --word;

            Assert.Equal(-357_913_942, newWord.Value);
        }

        [Fact]
        public void Add_Values_Correctly()
        {
            var word1 = new MixWord(7);
            var word2 = new MixWord(3);

            Assert.Equal(10, (word1 + word2).Value);
        }

        [Fact]
        public void Subtract_Values_Correctly()
        {
            var word1 = new MixWord(7);
            var word2 = new MixWord(3);

            Assert.Equal(4, (word1 - word2).Value);
        }

        [Fact]
        public void Negate_Values_Correctly()
        {
            var word1 = new MixWord(7);
            var word2 = -word1;
            var word3 = -word2;

            Assert.Equal(-7, word2.Value);
            Assert.Equal(7, word3.Value);
        }
    }
}