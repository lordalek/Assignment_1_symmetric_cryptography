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
            _cryptionKey.SetKey("1isAKey1", false);
           
        }

        [Test]
        public void shift1Left()
        {
            var input = "100000000000000b";
            var outputWith1Shift = _cryptionKey.ShiftUsingSB(input, 1, false);
            Assert.AreEqual("00000000000000b1", outputWith1Shift);
        }
        [Test]
        public void shift2Left()
        {
            var input = "100000000000000b";
            var outputWith1Shift = _cryptionKey.ShiftUsingSB(input, 3, false);
            Assert.AreEqual("0000000000000b10", outputWith1Shift);
        }

        [Test]
        public void shift1Right()
        {
            var input = "100000000000000b";
            var outputWith1Shift = _cryptionKey.ShiftUsingSB(input, 1, true);
            Assert.AreEqual("b100000000000000", outputWith1Shift);
        }

        [Test]
        public void shift2Right()
        {
            var input = "100000000000000b";
            var outputWith1Shift = _cryptionKey.ShiftUsingSB(input, 3, true);
            Assert.AreEqual("0b10000000000000", outputWith1Shift);
        }
    }
}
