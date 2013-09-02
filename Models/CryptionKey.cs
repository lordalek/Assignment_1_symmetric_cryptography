using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CryptionKey
    {
        public int KeySize { get { return 56; } }

        public string Key
        {
            get { return _key; }
        }

        private readonly string _key = string.Empty;

        public CryptionKey(string inputKey)
        {
            if(string.IsNullOrEmpty(inputKey))
                throw new  NullReferenceException("inputKey is null or empty");
            if(inputKey.Length != (KeySize/8))
                throw new Exception("Invalid input key lenth");
            _key = inputKey;
        }
    }
}
