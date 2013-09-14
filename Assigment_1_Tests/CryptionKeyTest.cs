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

        [Test]
        public void CheckIf16KeysAreGenerated()
        {
            string[] keys = new string[16];
            for (int round = 1; round <= 16; round++)
            {
                keys[round - 1] = _cryptionKey.GetKey(round);
            }
            Assert.IsNotNull(keys[0]);
            Assert.IsNotNull(keys[1]);
            Assert.IsNotNull(keys[2]);
            Assert.IsNotNull(keys[3]);
            Assert.IsNotNull(keys[5]);
            Assert.IsNotNull(keys[6]);
            Assert.IsNotNull(keys[10]);
            Assert.IsNotNull(keys[11]);
            Assert.IsNotNull(keys[15]);
            Assert.IsNotNull(keys[0]);
        }

        [Test]
        public void CheckThatKeysGetPermutated_1()
        {
            var Lkey = "1234567890abcdefghijklmnopqr";
            var RKey = "abcdefghijklmnopqr1234567890";
            var pt1 = _cryptionKey.PerformPC2("234567890abcdefghijklmnopqr1bcdefghijklmnopqr1234567890a");
            var st1 = _cryptionKey.GenerateSubKey(Lkey, RKey, 1);
            Assert.AreEqual(pt1, st1);
        }

        [Test]
        public void CheckThatKeysGetPermutated_2()
        {
            var Lkey = "234567890abcdefghijklmnopqr1";
            var RKey = "bcdefghijklmnopqr1234567890a";
            var st2 = _cryptionKey.GenerateSubKey(Lkey, RKey, 2);
            var pt2 = _cryptionKey.PerformPC2("34567890abcdefghijklmnopqr12cdefghijklmnopqr1234567890ab");
            Assert.AreEqual(pt2, st2);
        }
        [Test]
        public void CheckThatKeysGetPermutated_3()
        {
            var Lkey = "34567890abcdefghijklmnopqr12";
            var RKey = "cdefghijklmnopqr1234567890ab";
            var st3 = _cryptionKey.GenerateSubKey(Lkey, RKey, 3);
            var pt3 = _cryptionKey.PerformPC2("567890abcdefghijklmnopqr1234efghijklmnopqr1234567890abcd");
            Assert.AreEqual(pt3, st3);
        }
    }
}
