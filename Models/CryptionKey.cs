using System;
using System.Text;

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

        private string _key = string.Empty;

        public int KeySize
        {
            get { return 56; }
        }

        public string Key
        {
            get { return _key; }
        }

        public void SetKey(string inputKey, int round, bool isInverse)
        {
            if (string.IsNullOrEmpty(inputKey))
                throw new NullReferenceException("inputKey is null or empty");
            var block = new Block();
            if (round <= 1)
            {
                string binaryKey = block.ConvertStringToBinaryString(inputKey);
                binaryKey = PerformPC1(binaryKey);
                binaryKey = GetSevenBitsInKey(binaryKey);
                string[] keySplits = SplitKey(binaryKey);
                binaryKey = ShiftKey(keySplits[0], 1, isInverse) + ShiftKey(keySplits[1], 1, isInverse);
                binaryKey = PerformPC2(binaryKey);
                _key = binaryKey;
            }
            else
            {
                string binaryKey = inputKey;
                string[] keySplits = SplitKey(binaryKey);
                binaryKey = ShiftKey(keySplits[0], round, isInverse) + ShiftKey(keySplits[1], round, isInverse);
                binaryKey = PerformPC2(binaryKey);
                _key = binaryKey;
            }
        }

        public string GetSevenBitsInKey(string inputKey)
        {
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
            var sb = new StringBuilder();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    int idx = PC1[i, j];
                    sb.Append(nRoundKey[idx - 1]);
                }
            }
            return sb.ToString();
        }

        public string[] SplitKey(string inputKey)
        {
            if (inputKey.Length < KeySize)
                throw new Exception("Invalid input key length");
            var keySplits = new string[2];
            keySplits[0] = inputKey.Substring(0, KeySize/2 - 1);
            keySplits[1] = inputKey.Substring(KeySize/2, inputKey.Length - KeySize/2 - 1);

            return keySplits;
        }

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
            return shiftedKey.ToString();
        }
    }
}