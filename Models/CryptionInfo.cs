using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CryptionInfo : XmlSerializable
    {
        public string Key { get; set; }
        public string CipherBinary { get; set; }
        public string PlaintText { get; set; }
    }
}