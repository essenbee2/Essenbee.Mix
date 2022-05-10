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

            var val00 = word.Read(FieldSpec.Instance(0, 0)); // -1
            var val05 = word.Read(FieldSpec.Instance(0, 5)); // -357_913_941
            var val02 = word.Read(FieldSpec.Instance(0, 2)); // -1_365
            var val15 = word.Read(FieldSpec.Instance(1, 5)); // 357_913_941
            var val25 = word.Read(FieldSpec.Instance(2, 5)); // 5_592_405
            var val35 = word.Read(FieldSpec.Instance(3, 5)); // 87_381
            var val44 = word.Read(FieldSpec.Instance(4, 4)); // 21
            var val45 = word.Read(FieldSpec.Instance(4, 5)); // 1_365

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
            var word = new MixWord(-357_827_669); // [-|010101|010101|000000|010001|010101]
            var opString = word.ToOpString();

            Assert.Equal("[-|1365|I=00|F=(1:2)|Op=21]", opString);
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

        [Fact]
        public void Correctly_Set_Instruction()
        {
            var instruction = new MixWord();
            instruction.Write(8, 1000, FieldSpec.Instance(1, 5), 1);

            // Expect [+|1111101000|000001|101001|001000] -> [+|1000|I=01|F=(1:5)|Op=08]
            var opString1 = instruction.ToOpString();
            instruction.Write(8, -1000, FieldSpec.Default, 1);
            var opString2 = instruction.ToOpString();

            Assert.Equal("[+|1000|I=01|F=(1:5)|Op=08]", opString1);
            Assert.Equal("[-|1000|I=01|F=(0:5)|Op=08]", opString2);
        }

        [Fact]
        public void Correctly_Write_Data()
        {
            var memoryCell = new MixWord();
            memoryCell.Write(1000, FieldSpec.Instance(1, 2));
            Assert.Equal(1000, memoryCell[1, 2]);

            memoryCell = new MixWord();
            memoryCell.Write(1000, FieldSpec.Instance(0, 2));
            Assert.Equal(1000, memoryCell[0, 2]);
            memoryCell = new MixWord();
            memoryCell.Write(-1000, FieldSpec.Instance(0, 2));
            Assert.Equal(-1000, memoryCell[0, 2]);

            // Change sign
            memoryCell = new MixWord();
            memoryCell.Write(60, FieldSpec.Instance(1, 2));
            memoryCell.Write(-1, FieldSpec.Instance(0, 0));
            Assert.Equal(-60, memoryCell[0, 2]);

            // Ingore sign
            memoryCell = new MixWord();
            memoryCell.Write(-60, FieldSpec.Instance(1, 2));
            Assert.Equal(60, memoryCell[1, 2]);
            Assert.Equal(60, memoryCell[0, 2]); // Sign not to be set to -ve here!

            memoryCell = new MixWord();
            memoryCell.Write(60, FieldSpec.Instance(2, 2));
            Assert.Equal(60, memoryCell[2, 2]);

            // Write larger value into a 6-bit field
            memoryCell = new MixWord();
            memoryCell.Write(80, FieldSpec.Instance(2, 2)); // [000001|010000] -> [01|16]
            Assert.Equal(16, memoryCell[2, 2]);

            //Packed Word
            memoryCell = new MixWord();
            memoryCell.Write(10000, FieldSpec.Instance(1, 3));
            memoryCell.Write(3000, FieldSpec.Instance(4, 5));
            Assert.Equal(10000, memoryCell[1, 3]);
            Assert.Equal(3000, memoryCell[4, 5]);
        }

        [Fact]
        public void Read_Value_Correctly()
        {
            var word = new MixWord(0);
            word.Write(-1, FieldSpec.Instance(0, 0));
            word.Write(21, FieldSpec.Instance(1, 1));
            word.Write(21, FieldSpec.Instance(2, 2));
            word.Write(21, FieldSpec.Instance(3, 3));
            word.Write(21, FieldSpec.Instance(4, 4));
            word.Write(21, FieldSpec.Instance(5, 5));

            Assert.Equal(-357_913_941, word.Value);
        }
    }
}