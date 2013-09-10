using System;
using Models;
using Models.Annotations;

namespace Assignment_1_symmetric_cryptography
{
    public class CryptionLogic
    {
        public string[] PermutateTwoBlocks(string[] blocks)
        {
           if(string.IsNullOrEmpty(blocks[0]))
               throw  new Exception("Blocks is null or empty");
            var permutatedBlocks = new string[2];
            if(blocks.Length <= 1)
                throw new Exception("Blocks only contains one block");

            permutatedBlocks[0] = blocks[1];
            permutatedBlocks[1] = blocks[0];
            return permutatedBlocks;
        }

        public string Substitute(string block)
        {
            return string.Empty;
        }

        public string Encrypt(string plaintext64Bit, string keyString64Bit)
        {
            if (plaintext64Bit == null) throw new ArgumentNullException("plaintext64Bit");
            if (keyString64Bit == null) throw new ArgumentNullException("keyString64Bit");

            //Check that input has 8 bytes of data
            if(plaintext64Bit.Length < 8 || keyString64Bit.Length < 8)
                throw new Exception("Invalid plaintext or key length.");

            var keyModel = new CryptionKey();
            var blockModel = new Block();
            keyModel.SetKey(keyString64Bit, false);
            plaintext64Bit = blockModel.ConvertStringToBinaryString(plaintext64Bit);
            plaintext64Bit = blockModel.InitialPermutation(plaintext64Bit);
            var twoBlockOfPlainText = blockModel.SplitBlockIntoStrings(plaintext64Bit);
            for (var round = 1; round <= 16; round++)
            {

                twoBlockOfPlainText = blockModel.ExecuteRound(twoBlockOfPlainText[0], twoBlockOfPlainText[1],
                    keyModel.GetKey(round));
            }
            var cipherText = blockModel.ConvertBinariesToText(blockModel.InverseInitialPermutation(twoBlockOfPlainText[0] + twoBlockOfPlainText[1]));
            
            return cipherText;
        }

        public string Decrypt([NotNull] string cipherText, [NotNull] string key)
        {
            if (cipherText == null) throw new ArgumentNullException("cipherText");
            if (key == null) throw new ArgumentNullException("key");
            if (cipherText.Length < 8 || key.Length < 8)
                throw new Exception("Invalid plaintext or key length.");

            var keyModel = new CryptionKey();
            var blockModel = new Block();
            keyModel.SetKey(key, true);
            cipherText = blockModel.ConvertStringToBinaryString(cipherText);
            cipherText = blockModel.InitialPermutation(cipherText);
            var twoBlockOfPlainText = blockModel.SplitBlockIntoStrings(cipherText);
            for (var round = 1; round <= 16; round++)
            {

                twoBlockOfPlainText = blockModel.ExecuteRound(twoBlockOfPlainText[0], twoBlockOfPlainText[1],
                    keyModel.GetKey(round));
            }
            var plaintText = blockModel.ConvertBinariesToText(blockModel.InverseInitialPermutation(twoBlockOfPlainText[0] + twoBlockOfPlainText[1]));

            return plaintText;

        }
    }
}
