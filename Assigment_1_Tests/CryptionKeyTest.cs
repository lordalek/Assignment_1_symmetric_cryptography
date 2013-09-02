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
            _cryptionKey.SetKey("1isAKey1", 1, false);
        }

        [Test]
        public void CheckIfKeyIsSet()
        {
            Assert.AreEqual(_cryptionKey.Key, "0000000011111111100111100000010000100000100010010");
        }

        [Test]
        public void TestPC1()
        {
            //inputKey: 00110000110100011100101000000100101011001001111000011000
            //OutPutHex: 30D1CA04AC9E18
            var test = _cryptionKey.PerformPC1(_cryptionKey.Key);
            Assert.IsTrue(test.Contains("10"));
        }
    }
}
