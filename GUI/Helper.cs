using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.Annotations;

namespace GUI
{
    public static class Helper
    {
        public static CryptionInfo GetCryptionInfo(string path)
        {
            if (string.IsNullOrEmpty(path))
                return new CryptionInfo();

            var cryptionInfo = new CryptionInfo();
            cryptionInfo.Load(Path.Combine(path, "CryptionInfo.xml"));
            return cryptionInfo;
        }

        public static void SaveCryptionInfo([NotNull] CryptionInfo info, string path)
        {
            if (info == null) throw new ArgumentNullException("info");
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("path");
            info.Save(Path.Combine(path, "CryptionInfo.xml"));
        }
    }
}