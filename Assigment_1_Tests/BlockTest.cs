using System;
using System.Linq;
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
            Assert.IsTrue(_block.SplitBlockIntoStrings("abcd").Count == 1);
        }
        [Test]
        public void Insert40bitString_expect2blocks()
        {
            Assert.IsTrue(_block.SplitBlockIntoStrings("abcde").Count == 2);
        }

        [Test]
        public void Insert48bitString_expect2blocks()
        {
            Assert.IsTrue(_block.SplitBlockIntoStrings("abcdef").Count == 2);
        }

        [Test]
        public void Insert56bitString_expect2blocks()
        {
            Assert.IsTrue(_block.SplitBlockIntoStrings("abc123").Count == 2);
        }

        [Test]
        public void Insert64bitString_expect2blocks()
        {
            Assert.IsTrue(_block.SplitBlockIntoStrings("abcde12").Count == 2);
        }

        [Test]
        public void Insert24bitString_expect1blocks()
        {
            Assert.IsTrue(_block.SplitBlockIntoStrings("abc").Count == 1);
        }

        [Test]
        public void Insert16bitString_expect1blocks()
        {
            Assert.IsTrue(_block.SplitBlockIntoStrings("ab").Count == 1);
        }

        [Test]
        public void Insert8bitString_expect1blocks()
        {
            Assert.IsTrue(_block.SplitBlockIntoStrings("a").Count == 1);
        }

        [Test]
        public void ConvertAToBinary()
        {
            var letterA = System.Text.Encoding.UTF8.GetBytes("A");
            var binaryOfA = _block.ConvertStringToBitArray("A");
            Assert.IsTrue(letterA.SequenceEqual(binaryOfA));
        }

        [Test]
        public void ConvertLongStringToBinary()
        {
            var lotsOfLetters = System.Text.Encoding.UTF8.GetBytes("ABC123!!");
            var binaryOfLetters = _block.ConvertStringToBitArray("ABC123!!");
            Assert.IsTrue(lotsOfLetters.SequenceEqual(binaryOfLetters));
        }

        [Test]
        public void CheckIfExtraPaddingIsAdded()
        {
            var threeSignString = "ABC";
            var binaryOfLetters = _block.ConvertStringToBitArray(threeSignString);
            Assert.IsTrue(32 == _block.ConvertStringToBitArray(threeSignString).Count());
        }
    }
}
