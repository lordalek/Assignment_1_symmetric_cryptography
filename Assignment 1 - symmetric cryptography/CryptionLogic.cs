using System;
using Models;
using Models.Annotations;

namespace Assignment_1_symmetric_cryptography
{
    public class CryptionLogic
    {
        public string[] PermutateTwoBlocks(string[] blocks)
        {
            if (string.IsNullOrEmpty(blocks[0]))
                throw new Exception("Blocks is null or empty");
            var permutatedBlocks = new string[2];
            if (blocks.Length <= 1)
                throw new Exception("Blocks only contains one block");

            permutatedBlocks[0] = blocks[1];
            permutatedBlocks[1] = blocks[0];
            return permutatedBlocks;
        }

        /// <summary>
        /// @Decprecated
        /// </summary>
        /// <param name="plaintext64Bit"></param>
        /// <param name="keyString64Bit"></param>
        /// <returns></returns>
        public string Encrypt(string plaintext64Bit, string keyString64Bit)
        {
            if (plaintext64Bit == null) throw new ArgumentNullException("plaintext64Bit");
            if (keyString64Bit == null) throw new ArgumentNullException("keyString64Bit");

            //Check that input has 8 bytes of data
            if (plaintext64Bit.Length < 8 || keyString64Bit.Length < 8)
                throw new Exception("Invalid plaintext or key length.");

            var keyModel = new CryptionKey();
            var blockModel = new Block();
            keyModel.SetKey(keyString64Bit, false);
            plaintext64Bit = blockModel.ConvertStringToBinaryString(plaintext64Bit);
            plaintext64Bit = blockModel.InitialPermutation(plaintext64Bit);
            var shadowCopyLeft = new string[36];
            var shadowCopyRight = new string[36];
            var twoBlockOfPlainText = blockModel.SplitBlockIntoStrings(plaintext64Bit);
            shadowCopyLeft[0] = twoBlockOfPlainText[0];
            shadowCopyRight[0] = shadowCopyRight[0];
            for (var round = 1; round <= 16; round++)
            {
                shadowCopyLeft[round] = shadowCopyRight[round];
                var roundedBinaries = blockModel.ExecuteFunctionF(shadowCopyRight[round - 1],
                    keyModel.GetKey(round));
                shadowCopyRight[round] = blockModel.XORTwoBinaryStrings(roundedBinaries, shadowCopyLeft[round - 1]);
            }
            var cipherText = blockModel.InverseInitialPermutation(shadowCopyLeft[16] + shadowCopyRight[16]);
            return cipherText;
        }

        /// <summary>
        /// @Deprecated
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Decrypt([NotNull] string cipherText, [NotNull] string key)
        {
            if (cipherText == null) throw new ArgumentNullException("cipherText");
            if (key == null) throw new ArgumentNullException("key");
            if (cipherText.Length < 64 || key.Length < 8)
                throw new Exception("Invalid plaintext or key length.");

            var keyModel = new CryptionKey();
            var blockModel = new Block();
            keyModel.SetKey(key, false);
            //cipherText = blockModel.ConvertStringToBinaryString(cipherText);
            cipherText = blockModel.InverseInitialPermutation(cipherText);
            var twoBlockOfPlainText = blockModel.SplitBlockIntoStrings(cipherText);
            for (var round = 16; round > 0; round--)
            {
                //twoBlockOfPlainText = blockModel.ExecuteFunctionF(twoBlockOfPlainText[1],
                //    keyModel.GetKey(round));
            }
            //var plaintText = blockModel.ConvertBinariesToText(blockModel.InverseInitialPermutation(twoBlockOfPlainText[0] + twoBlockOfPlainText[1]));

            var plaintText = blockModel.InitialPermutation(twoBlockOfPlainText[1] + twoBlockOfPlainText[0]);

            var text = blockModel.ConvertBinariesToText(plaintText);
            return plaintText;
        }



        public string EncryptManual(string plaintText, string key)
        {
            var keyModel = new CryptionKey();
            var blockModel = new Block();
            keyModel.SetKey(key, false);
            var bin = blockModel.ConvertStringToBinaryString(plaintText);
            var permutedBin = blockModel.InitialPermutation(bin);
            var permutedbinBlocks = blockModel.SplitBlockIntoStrings(permutedBin);
            var leftSide = new string[17];
            leftSide[0] = permutedbinBlocks[0];
            var rightSide = new string[17];
            rightSide[0] = permutedbinBlocks[1];
            for (int i = 1; i <= 16; i++)
            {
                var roundBin = blockModel.ExecuteFunctionF(rightSide[i - 1], keyModel.GetKey(i));
                leftSide[i] = rightSide[i - 1];
                rightSide[i] = blockModel.XORTwoBinaryStrings(roundBin, leftSide[i - 1]);
            }
            var cipherText = blockModel.InverseInitialPermutation(rightSide[16] + leftSide[16]);
            return cipherText;
        }

        public string DecryptManual(string ciper, string key)
        {
            var keyModel = new CryptionKey();
            var blockModel = new Block();
            keyModel.SetKey(key, false);
            var bin = ciper;
            var permutedBin = blockModel.InitialPermutation(bin);
            var permutedbinBlocks = blockModel.SplitBlockIntoStrings(permutedBin);
            var leftSide = new string[17];
            leftSide[16] = permutedbinBlocks[0];
            var rightSide = new string[17];
            rightSide[16] = permutedbinBlocks[1];
            for (int i = 16; i >= 1; i--)
            {
                var roundBin = blockModel.ExecuteFunctionF(rightSide[i], keyModel.GetKey(i));
                leftSide[i - 1] = rightSide[i];
                rightSide[i - 1] = blockModel.XORTwoBinaryStrings(roundBin, leftSide[i]);
            }
            var cipherText = blockModel.InverseInitialPermutation(rightSide[0] + leftSide[0]);
            return cipherText;
        }
    }
}