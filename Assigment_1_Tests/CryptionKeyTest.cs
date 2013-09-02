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
            _cryptionKey = new CryptionKey("1isAKey");
        }

        [Test]
        public void CheckIfKeyIsSet()
        {
            Assert.AreEqual(_cryptionKey.Key, "1isAKey");
        }
    }
}
