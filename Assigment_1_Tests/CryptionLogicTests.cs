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
        public void InsertPlainttextIntoEncrpytion_expectSomeCipherText_withGenericKey()
        {
            var plainText = "abc123av";
            var key = "12345678";
            var cipherText = _logic.Encrypt(plainText, key);
            Assert.AreNotEqual(plainText, cipherText);
        }

        [Test]
        public void InsertCipherText()
        {
            var cipherText = "ଉȍ؋Є܏ȅ̌༈";
            var key = "12345678";
            var plaintText = _logic.Decrypt(cipherText, key);
            Assert.AreEqual(plaintText, "abc123av");
        }
    }
}
