using System.Collections.Generic;
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
        private Block _block;
        private CryptionKey _key;

        [SetUp]
        public void Init()
        {
            _logic = new CryptionLogic();
            _block = new Block();
            _key = new CryptionKey();
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
            var cipherText = "1011000000111011000110110001001010110001101110111001000010010001";
            var key = "12345678";
            var plaintText = _logic.Decrypt(cipherText, key);
            Assert.AreEqual(_block.ConvertStringToBinaryString("abc123av"), plaintText);
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
            var plainText = "abcd1234";
            var key = "12345678";
            Assert.AreEqual(_block.ConvertStringToBinaryString(plainText),
                _logic.Decrypt(_logic.Encrypt(plainText, key), key));
        }

        [Test]
        public void testManualEncrpytion()
        {
            var plain = "abcdfeqe";
            var key = "12345678";
            var cipher = _logic.EncryptManual(plain, key);
            Assert.IsTrue(cipher.Length == 64);
        }

        [Test]
        public void AssureOutPutOfEncryptAndManualEncryptAreEqual()
        {
            var manual = "1110001001101010111000100101011111000010111010001110111000001010";
            var plain = "abcdfeqe";
            var key = "12345678";
            var cipher = _logic.Encrypt(plain, key);
            Assert.AreEqual(manual, cipher);
        }

        [Test]
        public void testmanualecnrytAndDecrypt()
        {
            var manual = "1110001001101010111000100101011111000010111010001110111000001010";
            var key = "12345678";
            var plain = _block.ConvertStringToBinaryString("abcdfeqe");
            Assert.AreEqual(plain, _logic.Decrypt(manual, key));
        }

        [Test]
        public void testmanualecnrytAndDecryptasText()
        {
            var manual = "0010011010101000111010110000000111000100101000011101101100010000";
            var key = "12345678";
            var plain = "abcdfeqe";
            Assert.AreEqual("0110000101100010011000110110010001100110011001010111000101100101", _logic.DecryptManual(manual, key));
        }

        [Test]
        public void TEstManualDecrypt()
        {
            var key = "12345678";
            var plain = "abcdfeqe";
            Assert.AreEqual(_block.ConvertStringToBinaryString(plain), _logic.DecryptManual(_logic.Encrypt(plain, key), key));
        }

        [Test]
        public void CheckThatDecrpytedR16IsEncrpyt15()
        {
            var key = "12345678";
            var plainText = "abcdfeqe";
            var keyModel = new CryptionKey();
            keyModel.SetKey(key, false);
            var encryptedBin = _logic.EncryptManual(plainText, key);
            var initialPermutated = _block.InitialPermutation(encryptedBin);
            var splitBin = _block.SplitBlockIntoStrings(encryptedBin);
            var round16Decrpyt = _logic.performFuncionFAndXorWithLeft(splitBin[0], splitBin[1], _block,
                keyModel.GetKey(16));
            //round16Decrpyt = _block.InverseInitialPermutation(splitBin[0] + round16Decrpyt);
            var crypts = new List<string>();
            crypts.Add(_block.ConvertStringToBinaryString(plainText));
            for (int i = 1; i <= 16; i++)
            {
                var splits = _block.SplitBlockIntoStrings(crypts[i - 1]);
                crypts.Add(splits[1] + _logic.performFuncionFAndXorWithLeft(splits[0], splits[1], _block, keyModel.GetKey(i)));
            }
            round16Decrpyt = splitBin[0] + round16Decrpyt;
            Assert.AreEqual("0101111101100011010010110110100010110010101001011001010100110001", crypts[crypts.Count - 1]);
            Assert.AreEqual(crypts[crypts.Count - 2], round16Decrpyt);
        }

        [Test]
        public void CheckThatEncrpy17EqualsDecrpyt16()
        {
            var key = "12345678";
            var plainText = "abcdfeqe";
            var keyModel = new CryptionKey();
            keyModel.SetKey(key, false);
            var encryptedBin = _logic.EncryptManual(plainText, key);
            var initialPermutated = _block.InitialPermutation(encryptedBin);
            var splitBin = _block.SplitBlockIntoStrings(encryptedBin);
            Assert.AreEqual("0101111101100011010010110110100010110010101001011001010100110001", splitBin[1] + splitBin[0]);
        }


    }
}