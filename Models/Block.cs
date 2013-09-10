using System;
using System.Globalization;
using System.IO;
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

            if (inputBlock.Length > BlockSize)
                throw new Exception("inputBlock length is too long.");
            //var binarys = ConvertStringToBinaryString(inputBlock);
            string[] blocks;
            if (inputBlock.Length <= BlockSize/2)
            {
                blocks = new string[1];
                blocks[0] = inputBlock;
            }
            else
            {
                blocks = new string[2];
                blocks[0] = inputBlock.Substring(0, BlockSize/2);
                blocks[1] = inputBlock.Substring(BlockSize/2, BlockSize/2);
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
            var byteArray = Encoding.Unicode.GetBytes(inputLetter);
            byteArray = byteArray.Reverse().ToArray();
            var sb = new StringBuilder();
            foreach (var b in byteArray)
            {
                sb.Append(Convert.ToString(b, 2));
            }
            return sb.ToString().PadLeft(8, '0');
        }

        public string ConvertBinariesToText([NotNull] string binary)
        {
            if (binary == null) throw new ArgumentNullException("binary");
            if (binary.Length < 8)
                throw new Exception("Binary is too short");

            var sb = new StringBuilder();
            sb.Append(ConvertBinaryToLetter(binary.Substring(0, 8)));
            sb.Append(ConvertBinaryToLetter(binary.Substring(8, 8)));
            sb.Append(ConvertBinaryToLetter(binary.Substring(16, 8)));
            sb.Append(ConvertBinaryToLetter(binary.Substring(24, 8)));
            sb.Append(ConvertBinaryToLetter(binary.Substring(32, 8)));
            sb.Append(ConvertBinaryToLetter(binary.Substring(40, 8)));
            sb.Append(ConvertBinaryToLetter(binary.Substring(48, 8)));
            sb.Append(ConvertBinaryToLetter(binary.Substring(56, 8)));
            //var bytes = BitConverter.GetBytes(Convert.ToInt64(binary, 2)).Reverse().ToArray();
            //var test1 = Encoding.Unicode.GetString(bytes);
            ////var test = Convert.ToBase64String(bytes);
            //return Encoding.Unicode.GetString(hex);
            return sb.ToString();
        }

        public string ConvertBinaryToLetter([NotNull] string binary)
        {
            if (binary == null) throw new ArgumentNullException("binary");
            if (binary.Length != 8)
                return "-1";
            var bytes = ConvertHexToByte(ConvertBinaryToHex(binary));
            var text = Encoding.Unicode.GetString(bytes);
            return text;
        }

        public byte[] convertToByteArray(string bins)
        {
            var bytes = new byte[1];
            bytes[0] = Convert.ToByte(bins);
            return bytes;
        }

        public byte[] ConvertHexToByte(string hex)
        {
            int NumberChars = hex.Length;
            var bytes = new byte[NumberChars/2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i/2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
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
            //48 bit
            return sb.ToString();
        }

        public string XORTwoBinaryStrings([NotNull] string leftSide, [NotNull] string rightSide)
        {
            if (leftSide == null) throw new ArgumentNullException("leftSide");
            if (rightSide == null) throw new ArgumentNullException("rightSide");
            if (rightSide.Length != leftSide.Length)
                throw new Exception("Length of left and right side does not match");
            var sb = new StringBuilder();
            for (int i = 0; i < leftSide.Length; i++)
            {
                sb.Append(XorTwoChars(leftSide[i], rightSide[i]));
            }
            return sb.ToString();
        }

        public string SubstituteIntoSBox([NotNull] int[,] SBox, [NotNull] string inputText)
        {
            if (SBox == null) throw new ArgumentNullException("SBox");
            if (inputText == null) throw new ArgumentNullException("inputText");
            var rowCounter = Convert.ToInt32(inputText[0].ToString() + inputText[inputText.Length - 1].ToString(), 2);
            var columnCounter =
                Convert.ToInt32(
                    inputText[1].ToString() + inputText[2].ToString() + inputText[3].ToString() +
                    inputText[4].ToString(), 2);
            var sBoxValue = Convert.ToString(SBox[rowCounter, columnCounter], 2);
            return sBoxValue.PadLeft(4, '0');
        }

        public string Substitute48BitTextInto32BitTextUsingSBox([NotNull] string textOf48Bits)
        {
            if (textOf48Bits == null) throw new ArgumentNullException("textOf48Bits");
            if (textOf48Bits.Length != 48)
                throw new Exception("Invalid length of input text: " + textOf48Bits.Length);
            var sb = new StringBuilder();
            sb.Append(SubstituteIntoSBox(SBox1, textOf48Bits.Substring(0, 6)));
            sb.Append(SubstituteIntoSBox(SBox2, textOf48Bits.Substring(6, 6)));
            sb.Append(SubstituteIntoSBox(SBox3, textOf48Bits.Substring(12, 6)));
            sb.Append(SubstituteIntoSBox(SBox4, textOf48Bits.Substring(18, 6)));
            sb.Append(SubstituteIntoSBox(SBox5, textOf48Bits.Substring(24, 6)));
            sb.Append(SubstituteIntoSBox(SBox6, textOf48Bits.Substring(30, 6)));
            sb.Append(SubstituteIntoSBox(SBox7, textOf48Bits.Substring(36, 6)));
            sb.Append(SubstituteIntoSBox(SBox8, textOf48Bits.Substring(42, 6)));
            return sb.ToString();
        }

        public string XorTwoChars(char a, char b)
        {
            return a.Equals(b) ? "0" : "1";
        }

        public string Permutate32BitText([NotNull] string unPermutated32BitText)
        {
            if (unPermutated32BitText == null) throw new ArgumentNullException("unPermutated32BitText");
            var sb = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    var idx = PermutationFunctionTable[i, j];
                    sb.Append(unPermutated32BitText[idx - 1]);
                }
            }
            return sb.ToString();
        }

        public string InverseInitialPermutation([NotNull] string text64Bit)
        {
            if (text64Bit == null) throw new ArgumentNullException("text64Bit");
            var sb = new StringBuilder();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    var idx = InverseInitialPermutationTable[i, j];
                    sb.Append(text64Bit[idx - 1]);
                }
            }
            return sb.ToString();
        }

        public string[] ExecuteRound([NotNull] string left32BitText, [NotNull] string right32BitText,
            [NotNull] string roundKey)
        {
            if (left32BitText == null) throw new ArgumentNullException("left32BitText");
            if (right32BitText == null) throw new ArgumentNullException("right32BitText");
            if (roundKey == null) throw new ArgumentNullException("roundKey");
            var processedText = new string[2];
            processedText[0] = right32BitText;
            right32BitText = Expand32BitTextInto48BitText(right32BitText);
            //Now 48 bit text string
            right32BitText = XORTwoBinaryStrings(right32BitText, roundKey);
            right32BitText = Substitute48BitTextInto32BitTextUsingSBox(right32BitText);
            right32BitText = Permutate32BitText(right32BitText);
            right32BitText = XORTwoBinaryStrings(left32BitText, right32BitText);
            processedText[1] = right32BitText;
            return processedText;
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

        public readonly int[,] SBox1 =
        {
            {14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7},
            {0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8},
            {4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0},
            {15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13}
        };

        public readonly int[,] SBox2 =
        {
            {15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10},
            {3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5},
            {0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15},
            {13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9}
        };

        public readonly int[,] SBox3 =
        {
            {10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8},
            {13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1},
            {13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7},
            {1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12}
        };

        public readonly int[,] SBox4 =
        {
            {7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15},
            {13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9},
            {10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4},
            {3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14}
        };

        public readonly int[,] SBox5 =
        {
            {2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9},
            {14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6},
            {4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14},
            {11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3}
        };

        public readonly int[,] SBox6 =
        {
            {12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11},
            {10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8},
            {9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6},
            {4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13},
        };

        public readonly int[,] SBox7 =
        {
            {4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1},
            {13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6},
            {1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2},
            {6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12},
        };

        public readonly int[,] SBox8 =
        {
            {13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7},
            {1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2},
            {7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8},
            {2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11}
        };

        public string ConvertBinaryToHex(string binary)
        {
            if (binary.Length != 8)
                return "-1";
            var sb = new StringBuilder();
            sb.AppendFormat("{0:X2}", Convert.ToByte(binary.Substring(0, 4), 2));
            sb.AppendFormat("{0:X2}", Convert.ToByte(binary.Substring(4, 4), 2));
            return sb.ToString();
        }
    }
}