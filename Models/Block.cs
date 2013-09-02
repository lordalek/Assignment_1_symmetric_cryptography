using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Models
{
    public class Block
    {
        public int BlockSize { get { return 64; } }

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
                blocks[0] = binarys.Substring(0, BlockSize/2 -1);
                blocks[1] = binarys.Substring(BlockSize / 2, binarys.Length - BlockSize / 2 -1);
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
            byteArray= byteArray.Reverse().ToArray();
            var sb = new StringBuilder();
            foreach (var b in byteArray)
            {
                sb.Append(Convert.ToString(b, 2));
            }
            var binarystring = sb.ToString();
            return binarystring.PadLeft(8, '0');
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
    }
}
