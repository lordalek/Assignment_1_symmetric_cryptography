using System;
using System.Diagnostics;
using System.Text;
using Models.Annotations;

namespace Models
{
    public class CryptionKey
    {
        //Taken from Cryptography and Network Security - Prins and Pract. 5th ed - W. Stallings (Pearson, 2011)
        public readonly int[,] PC1 =
        {
            {57, 49, 41, 33, 25, 17, 9},
            {1, 58, 50, 42, 34, 26, 18},
            {10, 2, 59, 51, 43, 35, 27},
            {19, 11, 3, 60, 52, 44, 36},
            {63, 55, 47, 39, 31, 23, 15},
            {7, 62, 54, 46, 38, 30, 22},
            {14, 6, 61, 53, 45, 37, 29},
            {21, 13, 5, 28, 20, 12, 4}
        };

        //Taken from Cryptography and Network Security - Prins and Pract. 5th ed - W. Stallings (Pearson, 2011)
        public readonly int[,] PC2 =
        {
            {14, 17, 11, 24, 1, 5, 3, 28},
            {15, 6, 21, 10, 23, 19, 12, 4},
            {26, 8, 16, 7, 27, 20, 13, 2},
            {41, 52, 31, 37, 47, 55, 30, 40},
            {51, 45, 33, 48, 44, 49, 39, 56},
            {34, 53, 46, 42, 50, 36, 29, 32}
        };

        //Taken from Cryptography and Network Security - Prins and Pract. 5th ed - W. Stallings (Pearson, 2011)
        public readonly int[] ScheduledLeftShifts = {1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1};

        #region Variables

        private readonly string[] _subKeys = new string[16];
        private readonly string[] _shiftedKeysLeftSide = new string[16];
        private readonly string[] _shiftedKeysRightSide = new string[16];

        #endregion

        public int KeySize
        {
            get { return 56; }
        }

        public string GetKey(int roundNumber)
        {
            if (roundNumber <= 0 || string.IsNullOrEmpty(_subKeys[0]) || string.IsNullOrEmpty(_subKeys[15]))
                return "-1";

            return _subKeys[roundNumber - 1];
        }

        public void SetKey(string inputKey)
        {
            if (string.IsNullOrEmpty(inputKey))
                throw new NullReferenceException("inputKey is null or empty");
            var block = new Block();

            for (int round = 1; round <= 16; round++)
            {
                if (round <= 1)
                {
                    string binaryKey = block.ConvertStringToBinaryString(inputKey);
                    binaryKey = PerformPC1(binaryKey);
                    string[] keySplits = SplitKey(binaryKey);
                    _shiftedKeysLeftSide[round - 1] = ShiftSubKey(keySplits[0], round);
                    _shiftedKeysRightSide[round - 1] = ShiftSubKey(keySplits[1], round);
                    GenerateKey(_shiftedKeysLeftSide[round - 1], _shiftedKeysRightSide[round - 1], false, round);
                }
                else
                {
                    GenerateKey(_shiftedKeysLeftSide[round - 2], _shiftedKeysRightSide[round - 2], false, round);
                }
            }
        }

        private void GenerateKey([NotNull] string leftSide, [NotNull] string rightSide, bool isInverse, int round)
        {
            if (leftSide == null) throw new ArgumentNullException("leftSide");
            if (rightSide == null) throw new ArgumentNullException("rightSide");
            if (isInverse)
            {
                leftSide = ShiftSubKey(leftSide, round);
                rightSide = ShiftSubKey(rightSide, round);
                _shiftedKeysLeftSide[round - 1] = leftSide;
                _shiftedKeysRightSide[round - 1] = rightSide;
                leftSide = PerformPC2(leftSide + rightSide);
                _subKeys[round - 1] = leftSide;
            }
            else
            {
                leftSide = ShiftSubKey(leftSide, round);
                rightSide = ShiftSubKey(rightSide, round);
                _shiftedKeysLeftSide[round - 1] = leftSide;
                _shiftedKeysRightSide[round - 1] = rightSide;
                leftSide = PerformPC2(leftSide + rightSide);
                _subKeys[round - 1] = leftSide;
            }
        }

        public string GenerateSubKey([NotNull] string leftSide, [NotNull] string rightSide, int round)
        {
            leftSide = ShiftSubKey(leftSide, round);
            rightSide = ShiftSubKey(rightSide, round);
            _shiftedKeysLeftSide[round - 1] = leftSide;
            _shiftedKeysRightSide[round - 1] = rightSide;
            leftSide = PerformPC2(leftSide + rightSide);
            return leftSide;
        }

        public string PerformPC1(string firstRoundKey)
        {
            if (firstRoundKey.Length < 56)
                return "-1";
            var sb = new StringBuilder();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    int idx = PC1[i, j];
                    sb.Append(firstRoundKey[idx - 1]);
                }
            }
            return sb.ToString();
        }

        public string PerformPC2(string nRoundKey)
        {
            if (nRoundKey.Length < 48)
                return "-2";
            var sb = new StringBuilder();
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int idx = PC2[i, j];
                    sb.Append(nRoundKey[idx - 1]);
                }
            }
            return sb.ToString();
        }

        public string[] SplitKey(string inputKey)
        {
            if (inputKey.Length < 48)
                throw new Exception("Invalid input key length");
            var keySplits = new string[2];
            keySplits[0] = inputKey.Substring(0, KeySize/2);
            keySplits[1] = inputKey.Substring(KeySize/2, inputKey.Length - KeySize/2);
            return keySplits;
        }

        public string ShiftSubKey(string inputKey, int roundNumber)
        {
            if (inputKey == null) throw new ArgumentNullException("inputKey");
            int shiftDistance = ScheduledLeftShifts[roundNumber - 1];
            var sb = new StringBuilder();

            if (shiftDistance <= 1)
            {
                for (int i = 1; i < inputKey.Length; i++)
                {
                    sb.Append(inputKey[i]);
                }
                sb.Append(inputKey[0]);
            }
            else
            {
                for (int i = 2; i < inputKey.Length; i++)
                {
                    sb.Append(inputKey[i]);
                }
                sb.Append(inputKey[0]);
                sb.Append(inputKey[1]);
            }

            return sb.ToString();
        }
    }
}