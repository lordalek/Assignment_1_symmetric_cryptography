using Assignment_1_symmetric_cryptography;
using Models;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Assigment_1_Tests
{
    [TestFixture]
    public class CryptionLogicTests
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

        [Test]
        public void InsertAndExtract()
        {
            var plain = "abcdfeqe";
            var key = "12345678";
            var cryptedPlainBin = "1101000110010101110100011010101111000001110101001101110100000101";
            Assert.AreEqual(_logic.Encrypt(plain, key), cryptedPlainBin);
            //Assert.AreEqual(_logic.Decrypt(cryptedPlainBin, key), plain);
        }

        [Test]
        public void InsertBinExpectBin()
        {
            var cryptedPlainBin = "1101000110010101110100011010101111000001110101001101110100000101";
            var plainBin = "0110000101100010011000110110010001100110011001010111000101100101";

            var key = "12345678";
            Assert.AreEqual(plainBin, _logic.Decrypt(cryptedPlainBin, key));
        }
    }
}
