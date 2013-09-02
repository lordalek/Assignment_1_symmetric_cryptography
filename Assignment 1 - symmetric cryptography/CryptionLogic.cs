using System;

namespace Assignment_1_symmetric_cryptography
{
    public class CryptionLogic
    {
        public string[] PermutateTwoBlocks(string[] blocks)
        {
           if(string.IsNullOrEmpty(blocks[0]))
               throw  new Exception("Blocks is null or empty");
            var permutatedBlocks = new string[2];
            if(blocks.Length <= 1)
                throw new Exception("Blocks only contains one block");

            permutatedBlocks[0] = blocks[1];
            permutatedBlocks[1] = blocks[0];
            return permutatedBlocks;
        }

        public string Substitute(string block)
        {
            return string.Empty;
        }
    }
}
