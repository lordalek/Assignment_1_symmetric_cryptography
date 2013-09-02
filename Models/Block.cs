using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Block
    {
        public int BlockSize { get { return 64; } }

        public List<string> SplitBlockIntoStrings(string inputBlock)
        {
            if (string.IsNullOrEmpty(inputBlock))
                throw new NullReferenceException("InputBlock is null or empty");

            if (inputBlock.Length > BlockSize/8)
                throw new Exception("inputBlock length is too long.");

            var blocks = new List<string>();

            if (inputBlock.Length <= (BlockSize/8)/2)
                blocks.Add(inputBlock);
            else
            {
                blocks.Add(inputBlock.Substring(0,1));
                blocks.Add(inputBlock.Substring(1,2));
            }
            return blocks;
        }

        public byte[] ConvertStringToBitArray(string intputString)
        {
            if(string.IsNullOrEmpty(intputString))
                throw new NullReferenceException("inputString is null or empty");
            var binary = Encoding.UTF8.GetBytes(intputString);
            if (binary.Count() < 32)
            {
                binary = addPaddin(binary);
            }
            return binary;
        }

        private byte[] addPaddin(byte[] binaries)
        {
            var tmpArray = new byte[32];
            if (binaries == null || binaries.Count() <= 0)
                return tmpArray;
            for (var i = 0; i < binaries.Length; i++)
            {
                tmpArray[i] = binaries[i];
            }

            for (var i = binaries.Length; i <= 32; i++)
            {
                tmpArray[i] = 0;
            }

            return tmpArray;
        }
    }
}
