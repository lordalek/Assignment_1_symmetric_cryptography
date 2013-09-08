using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Models.Annotations;

namespace Models
{
    public class Block
    {
        public int BlockSize
        {
            get { return 64; }
        }

        public string[] SplitBlockIntoStrings(string inputBlock)
        {
            if (string.IsNullOrEmpty(inputBlock))
                throw new NullReferenceException("InputBlock is null or empty");

            if (inputBlock.Length > BlockSize/8)
                throw new Exception("inputBlock length is too long.");
            var binarys = ConvertStringToBinaryString(inputBlock);
            string[] blocks;
            if (binarys.Length <= BlockSize/2)
            {
                blocks = new string[1];
                blocks[0] = binarys;
            }
            else
            {
                blocks = new string[2];
                blocks[0] = binarys.Substring(0, BlockSize/2 - 1);
                blocks[1] = binarys.Substring(BlockSize/2, binarys.Length - BlockSize/2 - 1);
                //make sure block is 32 bits long.
                blocks[1] = blocks[1].PadRight(BlockSize/2, '0');
            }
            return blocks;
        }

        public string ConvertSingleLetterToBinaryString(char inputChar)
        {
            string inputLetter = inputChar.ToString(CultureInfo.InvariantCulture);
            if (string.IsNullOrEmpty(inputLetter))
                throw new NullReferenceException("inputLetter is null or empty");
            var byteArray = Encoding.UTF8.GetBytes(inputLetter);
            byteArray = byteArray.Reverse().ToArray();
            var sb = new StringBuilder();
            foreach (var b in byteArray)
            {
                sb.Append(Convert.ToString(b, 2));
            }
            return sb.ToString().PadLeft(8, '0');
        }

        public string ConvertStringToBinaryString(string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
                throw new NullReferenceException("inputString is null or empty");

            var sb = new StringBuilder();
            foreach (char c in inputString)
            {
                sb.Append(ConvertSingleLetterToBinaryString(c));
            }

            return sb.ToString().PadLeft(inputString.Length, '0');
        }

        public string InitialPermutation([NotNull] string inputText)
        {
            if (inputText == null) throw new ArgumentNullException("inputText");
            var sb = new StringBuilder();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    var idx = InitialPermutationTable[i, j];
                    sb.Append(inputText[idx - 1]);
                }
            }
            return sb.ToString();
        }

        public string Expand32BitTextInto48BitText([NotNull] string TxtWith32BitSize)
        {
            if (TxtWith32BitSize == null) throw new ArgumentNullException("TxtWith32BitSize");
            if (TxtWith32BitSize.Length != 32)
                throw new Exception("Invalid txt length: " + TxtWith32BitSize.Length);
            var sb = new StringBuilder();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    var idx = ExpansionPermutationTable[i, j];
                    sb.Append(TxtWith32BitSize[idx - 1]);
                }
            }
            return sb.ToString();
        }

        public string XORTwoBinaryStrings([NotNull] string leftSide, [NotNull] string rightSide )
        {
            if (leftSide == null) throw new ArgumentNullException("leftSide");
            if (rightSide == null) throw new ArgumentNullException("rightSide");
            if(rightSide.Length != leftSide.Length)
                throw new Exception("Length of left and right side does not match");
            var sb = new StringBuilder();
            for (int i = 0; i < leftSide.Length; i++)
            {
                sb.Append(XorTwoChars(leftSide[i], rightSide[i]));
            }
            return sb.ToString();
        }

        public string XorTwoChars(char a, char b)
        {
            return a.Equals(b) ? "0" : "1";
        }

        public readonly int[,] InitialPermutationTable =
        {
            {58, 50, 42, 34, 26, 18, 10, 2},
            {60, 52, 44, 36, 28, 20, 12, 4,},
            {62, 54, 46, 38, 30, 22, 14, 6},
            {64, 56, 48, 40, 32, 24, 16, 8},
            {57, 49, 41, 33, 25, 17, 9, 1},
            {59, 51, 43, 35, 27, 19, 11, 3},
            {61, 53, 45, 37, 29, 21, 13, 5},
            {63, 55, 47, 39, 31, 23, 15, 7}
        };

        public readonly int[,] InverseInitialPermutationTable =
        {
            {40, 8, 48, 16, 56, 24, 64, 32},
            {39, 7, 47, 15, 55, 23, 63, 31},
            {38, 6, 46, 14, 54, 22, 62, 30},
            {37, 5, 45, 13, 53, 21, 61, 29},
            {36, 4, 44, 12, 52, 20, 60, 28},
            {35, 3, 43, 11, 51, 19, 59, 27},
            {34, 2, 42, 10, 50, 18, 58, 26},
            {33, 1, 41, 9, 49, 17, 57, 25}
        };

        public readonly int[,] ExpansionPermutationTable =
        {
            {32, 1, 2, 3, 4, 5},
            {4, 5, 6, 7, 8, 9},
            {8, 9, 10, 11, 12, 13},
            {12, 13, 14, 15, 16, 17},
            {16, 17, 18, 19, 20, 21},
            {20, 21, 22, 23, 24, 25},
            {24, 25, 26, 27, 28, 29},
            {28, 29, 30, 31, 32, 1}
        };

        public readonly int[,] PermutationFunctionTable =
        {
            {16, 7, 20, 21, 29, 12, 28, 17},
            {1, 15, 23, 26, 5, 18, 31, 10},
            {2, 8, 24, 14, 32, 27, 3, 9},
            {19, 13, 30, 6, 22, 11, 4, 25}
        };
    }
}