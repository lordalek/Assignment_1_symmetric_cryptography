using System;
using Models;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Assigment_1_Tests
{
    [TestFixture]
    public class BlockTest
    {
        private Block _block;
        [SetUp]
        public void Init()
        {
            _block = new Block();
        }
        [Test]
        public void Insert32bitString_expect1block()
        {
            Assert.IsTrue(_block.SplitBlockIntoStrings("abcd").Length == 1);
        }
        [Test]
        public void Insert40bitString_expect2blocks()
        {
            Assert.IsTrue(_block.SplitBlockIntoStrings(_block.ConvertStringToBinaryString("abcde")).Length == 2);
        }

        [Test]
        public void Insert48bitString_expect2blocks()
        {
            Assert.IsTrue(_block.SplitBlockIntoStrings(_block.ConvertStringToBinaryString("abcdef")).Length == 2);
        }

        [Test]
        public void Insert56bitString_expect2blocks()
        {
            Assert.IsTrue(_block.SplitBlockIntoStrings(_block.ConvertStringToBinaryString("abc123")).Length == 2);
        }

        [Test]
        public void Insert64bitString_expect2blocks()
        {
            Assert.IsTrue(_block.SplitBlockIntoStrings(_block.ConvertStringToBinaryString("abcde12")).Length == 2);
        }

        [Test]
        public void Insert24bitString_expect1blocks()
        {
            Assert.IsTrue(_block.SplitBlockIntoStrings(_block.ConvertStringToBinaryString("abc")).Length == 1);
        }

        [Test]
        public void Insert16bitString_expect1blocks()
        {
            Assert.IsTrue(_block.SplitBlockIntoStrings("ab").Length == 1);
        }

        [Test]
        public void Insert8bitString_expect1blocks()
        {
            Assert.IsTrue(_block.SplitBlockIntoStrings("a").Length == 1);
        }

        [Test]
        public void ConvertAToBinary()
        {
            const int byteA = 65;
            var bitString = Convert.ToString(byteA, 2).PadLeft(8, '0');
            var binaryOfA = _block.ConvertSingleLetterToBinaryString('A');
            //Assert.IsTrue(binaryOfA.Equals(letterA));
            Assert.AreEqual(bitString, binaryOfA);
        }

        [Test]
        public void ConvertLongStringToBinary()
        {
            const string bitString = "0100000101000010010000110011000100110010001100110010000100100001";
            var binaryOfLetters = _block.ConvertStringToBinaryString("ABC123!!");
            Assert.AreEqual(bitString, binaryOfLetters);
        }

        [Test]
        public void TestLengthOfInitialPermutation()
        {
            var input = "abcasdwe";
            var inputAsByes = _block.ConvertStringToBinaryString(input);
            var permutated = _block.InitialPermutation(inputAsByes);
            Assert.IsTrue(inputAsByes.Length == permutated.Length);
        }

        [Test]
        public void CheckSizeOfExpansionOfETable()
        {
            var input = _block.ConvertStringToBinaryString("abc1");
            input = _block.Expand32BitTextInto48BitText(input);
            Assert.IsTrue(input.Length == 48);
        }

        [Test]
        public void SeeIfXORFlips8BitString()
        {
            var input = "11001010";
            var flippedInput = "00110101";
            Assert.AreEqual(flippedInput, _block.XORTwoBinaryStrings(input, "11111111"));
        }

        [Test]
        public void Insert6BitText_Expect4BitText()
        {
            var input = "100101";
            Assert.IsTrue(_block.SubstituteIntoSBox(_block.SBox1, input).Length == 4);
        }

        [Test]
        public void Insert48BitTextIntoSBox_Expect32BiText()
        {
            var input = _block.ConvertStringToBinaryString("abcasd");
            Assert.AreEqual(_block.Substitute48BitTextInto32BitTextUsingSBox(input).Length ,32);
        }

        [Test]
        public void Permutate32BitText_ExpectNotEqualTexts()
        {
            var input = _block.ConvertStringToBinaryString("ABcd");
            var permutedText = _block.Permutate32BitText(input);
            Assert.AreNotEqual(input, permutedText);
        }

        [Test]
        public void InsertAAsBinary_ExpectLetterA()
        {
            var AasBinary = _block.ConvertStringToBinaryString("ABCabc12");
            var letterA = _block.ConvertBinariesToText(AasBinary);
            Assert.AreEqual(letterA, "ABCabc12");
        }

        [Test]
        public void ChecckThatIpisIIP()
        {
            var plainText = "abcd1234";
            var plainBin = _block.ConvertStringToBinaryString(plainText);
            var IPbin = _block.InitialPermutation(plainBin);
            var IIPbin = _block.InverseInitialPermutation(IPbin);
            Assert.AreEqual(plainBin, IIPbin);
        }

        [Test]
        public void BS()
        {
            //var bin = "123abcdefghijklmnioqrstuw1234567890!#¤%&/()=:;<>_.,'*¨*`)(/&%¤#¤";
            var bin = "0011000000000000000000000000000000000000000000000000000000000000";
            var binIP = _block.InitialPermutation(bin);
            var binIIP = _block.InverseInitialPermutation(binIP);
            Assert.AreEqual(bin, binIIP);
        }
    }
}
