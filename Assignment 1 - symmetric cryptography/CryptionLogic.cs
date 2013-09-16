using System;
using Models;
using Models.Annotations;

namespace Assignment_1_symmetric_cryptography
{
    public class CryptionLogic
    {
        private CryptionKey _keyModel;
        private Block _blockModel;


        public string Encrpyt([NotNull] string plainText, [NotNull] string key)
        {
            if (key == null || key.Length <= 0) throw new ArgumentNullException("key");
            if (plainText == null || plainText.Length <= 0) throw new ArgumentNullException("plainText");

            #region Models

            _keyModel = new CryptionKey();
            _blockModel = new Block();
            _keyModel.SetKey(key, false);

            #endregion

            #region Variables

            var leftSide = new string[17];
            var rightSide = new string[17];

            #endregion

            #region Convert plainText to binary and split into two blocks

            var plainTextAsBinary = _blockModel.ConvertStringToBinaryString(plainText);
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

            return rightSide[16] + leftSide[16];
        }

        public string Decrpyt1(string cipherTextAsBinary, string key)
        {
            #region Models

            _keyModel = new CryptionKey();
            _blockModel = new Block();
            _keyModel.SetKey(key, false);

            #endregion

            #region Variables

            var leftSide = new string[17];
            var rightSide = new string[17];

            #endregion

            #region Initialize Variables

            var plainTextBinariesSplitInTwo = _blockModel.SplitBlockIntoStrings(cipherTextAsBinary);
            leftSide[16] = plainTextBinariesSplitInTwo[0];
            rightSide[16] = plainTextBinariesSplitInTwo[1];

            #endregion

            #region Execute round function

            for (int i = 16; i >= 1; i--)
            {
                rightSide[i - 1] = _blockModel.XORTwoBinaryStrings(leftSide[i],
                    FunctionF(rightSide[i], _keyModel.GetKey(i)));
                leftSide[i - 1] = rightSide[i];
            }

            #endregion

            return rightSide[0] + leftSide[0];
        }

        public string FunctionF(string dataBlock, string key)
        {
            dataBlock = _blockModel.Expand32BitTextInto48BitText(dataBlock);
            dataBlock = _blockModel.XORTwoBinaryStrings(dataBlock, key);
            return _blockModel.Substitute48BitTextInto32BitTextUsingSBox(dataBlock);
        }
    }
}