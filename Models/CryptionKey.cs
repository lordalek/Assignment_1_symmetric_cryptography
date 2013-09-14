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

        private readonly string[] _subKeys = new string[16];
        private readonly string[] _shiftedKeysLeftSide = new string[16];
        private readonly  string[] _shiftedKeysRightSide = new string[16];

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

        public void SetKey(string inputKey, bool isInverse)
        {
            if (string.IsNullOrEmpty(inputKey))
                throw new NullReferenceException("inputKey is null or empty");
            var block = new Block();
            if (isInverse)
            {
                for (int round = 1; round <= 16; round++)
                {
                    if (round <= 1)
                    {
                        string binaryKey = block.ConvertStringToBinaryString(inputKey);
                        binaryKey = PerformPC1(binaryKey);
                        string[] keySplits = SplitKey(binaryKey);
                        _shiftedKeysLeftSide[round - 1] = ShiftUsingSB(keySplits[0], round, true);
                        _shiftedKeysRightSide[round - 1] = ShiftUsingSB(keySplits[1], round, true);
                        //binaryKey = GetSevenBitsInKey(binaryKey);
                        GenerateKey(_shiftedKeysLeftSide[round - 1],_shiftedKeysRightSide[round - 1], true, round);
                    }
                    else
                    {
                        GenerateKey(_shiftedKeysLeftSide[round - 2] , _shiftedKeysRightSide[round -2], true, round);
                    }
                }
            }
            else
            {
                for (int round = 1; round <= 16; round++)
                {
                    if (round <= 1)
                    {
                        string binaryKey = block.ConvertStringToBinaryString(inputKey);
                        binaryKey = PerformPC1(binaryKey);
                        string[] keySplits = SplitKey(binaryKey);
                        _shiftedKeysLeftSide[round - 1] = ShiftUsingSB(keySplits[0], round, false);
                        _shiftedKeysRightSide[round - 1] = ShiftUsingSB(keySplits[1], round, false);
                        GenerateKey(_shiftedKeysLeftSide[round -1], _shiftedKeysRightSide[round -1], false, round);
                    }
                    else
                    {
                        GenerateKey(_shiftedKeysLeftSide[round - 2], _shiftedKeysRightSide[round - 2], false, round);
                    }
                }
            }
        }

        private void GenerateKey([NotNull] string leftSide, [NotNull] string rightSide, bool isInverse, int round)
        {
            if (leftSide == null) throw new ArgumentNullException("leftSide");
            if (rightSide == null) throw new ArgumentNullException("rightSide");
            if (isInverse)
            {
                leftSide = ShiftUsingSB(leftSide, round, true);
                rightSide = ShiftUsingSB(rightSide, round, true);
                _shiftedKeysLeftSide[round - 1] = leftSide;
                _shiftedKeysRightSide[round - 1] = rightSide;
                leftSide = PerformPC2(leftSide + rightSide);
                _subKeys[round - 1] = leftSide;
            }
            else
            {
                leftSide = ShiftUsingSB(leftSide, round, false);
                rightSide = ShiftUsingSB(rightSide, round, false);
                _shiftedKeysLeftSide[round - 1] = leftSide;
                _shiftedKeysRightSide[round - 1] = rightSide;
                leftSide = PerformPC2(leftSide + rightSide);
                _subKeys[round - 1] = leftSide;
            }
        }

        public string GenerateSubKey([NotNull] string leftSide, [NotNull] string rightSide, int round)
        {
            leftSide = ShiftUsingSB(leftSide, round, false);
            rightSide = ShiftUsingSB(rightSide, round, false);
            _shiftedKeysLeftSide[round - 1] = leftSide;
            _shiftedKeysRightSide[round - 1] = rightSide;
            leftSide = PerformPC2(leftSide + rightSide);
            return leftSide;
        }

        public string GetSevenBitsInKey(string inputKey)
        {
            if (inputKey.Length != 64)
            {
                return "-1";
            }
            var sp = new StringBuilder();
            int i = 0;
            for (int j = 0; j < 8; j++)
            {
                for (int k = 0; k < 8; k++)
                {
                    if (k != 7)
                        sp.Append(inputKey[i]);
                    i++;
                }
            }
            return sp.ToString();
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

        /// <summary>
        /// @Deprecated
        /// </summary>
        /// <param name="inputKey"></param>
        /// <param name="roundNumber"></param>
        /// <param name="isInverse"></param>
        /// <returns></returns> 
        public string ShiftKey(string inputKey, int roundNumber, bool isInverse)
        {
            int shiftDistance = ScheduledLeftShifts[roundNumber];
            var shiftedKey = new char[KeySize/2];
            if (isInverse)
            {
                /**
                 * Implement shift right for inverse
                 */
            }
            else
            {
                if (shiftDistance >= 1)
                {
                    for (int i = 0; i < inputKey.Length; i++)
                    {
                        if (i == 0)
                            shiftedKey[KeySize/2 - shiftDistance] = inputKey[i];
                        else
                        {
                            shiftedKey[i] = inputKey[i - shiftDistance];
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < inputKey.Length; i++)
                    {
                        if (i >= 2)
                        {
                            shiftedKey[i] = inputKey[i - shiftDistance];
                        }
                        else if (i == 0)
                            shiftedKey[KeySize/2 - shiftDistance] = shiftedKey[i];
                        else if (i == 1)
                            shiftedKey[KeySize/2 - shiftDistance/2] = shiftedKey[i];
                    }
                }
            }
            return new string(shiftedKey);
        }

        public string ShiftUsingSB(string inputKey, int roundNumber, bool isInverse)
        {
            if (inputKey == null) throw new ArgumentNullException("inputKey");
            int shiftDistance = ScheduledLeftShifts[roundNumber -1];
            var sb = new StringBuilder();
            if (isInverse)
            {
                //Right
                if (shiftDistance <= 1)
                {
                    sb.Append(inputKey[inputKey.Length - 1]);
                    for (int i = 0; i < inputKey.Length - 1; i++)
                    {
                        sb.Append(inputKey[i]);
                    }
                }
                else
                {
                    sb.Append(inputKey[inputKey.Length - 2]);
                    sb.Append(inputKey[inputKey.Length - 1]);
                    for (int i = 0; i < inputKey.Length - 2; i++)
                    {
                        sb.Append(inputKey[i]);
                    }
                }
            }
            else
            {
                //Left
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
            }
            return sb.ToString();
        }
    }
}