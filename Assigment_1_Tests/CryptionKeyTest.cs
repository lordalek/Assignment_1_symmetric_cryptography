using Models;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Assigment_1_Tests
{
    class CryptionKeyTest
    {
        private CryptionKey _cryptionKey;

        [SetUp]
        public void Init()
        {
            _cryptionKey = new CryptionKey();
        }

        [Test]
        public void CheckIfKeyIsSet()
        {
            _cryptionKey.Key = "1isAKey";
            Assert.AreEqual(_cryptionKey.Key, "00110001011010010111001101000001010010110110010101111001");
        }
    }
}
