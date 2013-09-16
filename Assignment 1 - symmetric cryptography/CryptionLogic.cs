using System;
using System.Text;
using Models;
using Models.Annotations;

namespace Assignment_1_symmetric_cryptography
{
    public class CryptionLogic
    {
        private CryptionKey _keyModel;
        private Block _blockModel;


        public string Encrypt([NotNull] string plainText, [NotNull] string key)
        {
            if (key == null || key.Length <= 0) throw new ArgumentNullException("key");
            if (plainText == null || plainText.Length <= 0) throw new ArgumentNullException("plainText");

            #region Models

            _keyModel = new CryptionKey();
            _blockModel = new Block();
            _keyModel.SetKey(key);
            var outBinaries = new StringBuilder();

            #endregion

            #region Round

            for (var index = 0; index < plainText.Length; index += 8)
            {
                string plaintTextBlock;
                if (index > plainText.Length - 8)
                {
                    plaintTextBlock = plainText.Substring(index,
                        plainText.Length - index);
                }
                else plaintTextBlock = plainText.Substring(index, 8);

                #region Variables

                var leftSide = new string[17];
                var rightSide = new string[17];

                #endregion

                #region Convert plainText to binary and split into two blocks

                var plainTextAsBinary = _blockModel.ConvertStringToBinaryString(plaintTextBlock);
                var plainTextBinariesSplitInTwo = _blockModel.SplitBlockIntoStrings(plainTextAsBinary);

                #endregion

                #region Initialize left and right blocks

                leftSide[0] = plainTextBinariesSplitInTwo[0];
                rightSide[0] = plainTextBinariesSplitInTwo[1];

                #endregion

                #region Execute the round function

                for (int i = 1; i <= 16; i++)
                {
                    rightSide[i] = _blockModel.XORTwoBinaryStrings(leftSide[i - 1],
                        FunctionF(rightSide[i - 1], _keyModel.GetKey(i)));
                    leftSide[i] = rightSide[i - 1];
                }

                #endregion

                outBinaries.Append(rightSide[16] + leftSide[16]);
            }

            #endregion

            return outBinaries.ToString();
        }

        public string Decrypt([NotNull] string cipherTextAsBinary, [NotNull] string key)
        {
            if (key == null || key.Length < 8) throw new ArgumentNullException("key");
            if (cipherTextAsBinary == null || cipherTextAsBinary.Length <= 0)
                throw new ArgumentNullException("cipherTextAsBinary");

            #region Models

            _keyModel = new CryptionKey();
            _blockModel = new Block();
            _keyModel.SetKey(key);
            var outBinaries = new StringBuilder();

            #endregion

            #region Round

            for (var index = 0; index < cipherTextAsBinary.Length; index += 64)
            {
                if ((cipherTextAsBinary.Length - index)%64 != 0)
                    throw new Exception("Invalid ciphertext length. Length must of length 64 * n");

                var cipherSubString = cipherTextAsBinary.Substring(index, 64);

                #region Variables

                var leftSide = new string[17];
                var rightSide = new string[17];

                #endregion

                #region Initialize Variables

                var plainTextBinariesSplitInTwo = _blockModel.SplitBlockIntoStrings(cipherSubString);
                leftSide[16] = plainTextBinariesSplitInTwo[0];
                rightSide[16] = plainTextBinariesSplitInTwo[1];

                #endregion

                #region Execute round function

                for (var i = 16; i >= 1; i--)
                {
                    rightSide[i - 1] = _blockModel.XORTwoBinaryStrings(leftSide[i],
                        FunctionF(rightSide[i], _keyModel.GetKey(i)));
                    leftSide[i - 1] = rightSide[i];
                }

                #endregion

                outBinaries.Append(rightSide[0] + leftSide[0]);
            }

            #endregion

            return outBinaries.ToString();
        }

        public string FunctionF(string dataBlock, string key)
        {
            dataBlock = _blockModel.Expand32BitTextInto48BitText(dataBlock);
            dataBlock = _blockModel.XORTwoBinaryStrings(dataBlock, key);
            dataBlock = _blockModel.Substitute48BitTextInto32BitTextUsingSBox(dataBlock);
            return _blockModel.Permutate32BitText(dataBlock);
        }
    }
}