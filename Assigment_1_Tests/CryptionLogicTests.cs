using Assignment_1_symmetric_cryptography;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Assigment_1_Tests
{
    class CryptionLogicTests
    {
        private CryptionLogic _logic;

        [SetUp]
        public void Init()
        {
            _logic = new CryptionLogic();
        }
    }
}
