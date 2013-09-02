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
            Assert.IsTrue(_block.SplitBlockIntoStrings("abcde").Length == 2);
        }

        [Test]
        public void Insert48bitString_expect2blocks()
        {
            Assert.IsTrue(_block.SplitBlockIntoStrings("abcdef").Length == 2);
        }

        [Test]
        public void Insert56bitString_expect2blocks()
        {
            Assert.IsTrue(_block.SplitBlockIntoStrings("abc123").Length == 2);
        }

        [Test]
        public void Insert64bitString_expect2blocks()
        {
            Assert.IsTrue(_block.SplitBlockIntoStrings("abcde12").Length == 2);
        }

        [Test]
        public void Insert24bitString_expect1blocks()
        {
            Assert.IsTrue(_block.SplitBlockIntoStrings("abc").Length == 1);
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
    }
}
