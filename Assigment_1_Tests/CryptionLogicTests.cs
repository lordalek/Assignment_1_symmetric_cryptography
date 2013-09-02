using Assignment_1_symmetric_cryptography;
using Models;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Assigment_1_Tests
{
    class CryptionLogicTests
    {
        private CryptionLogic _logic;

        [SetUp]
        public void Init()
        {
            _logic = new CryptionLogic();
        }

        [Test]
        public void TestPermutate()
        {
            var block = new Block();
            var blocks = block.SplitBlockIntoStrings("ABCD1234");
            var permutatedBlocks = _logic.PermutateTwoBlocks(blocks);
            Assert.AreNotEqual(blocks, permutatedBlocks);
            Assert.AreEqual(blocks[1], permutatedBlocks[0]);
            Assert.AreEqual(blocks[0], permutatedBlocks[1]);
        }

        [Test]
        public void TestSubstitution()
        {
            
        }
    }
}
