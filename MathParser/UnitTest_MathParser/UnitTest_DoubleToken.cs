using System;
using MathParser.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest_MathParser
{
    [TestClass]
    public class UnitTest_DoubleToken
    {
        [TestMethod]
        public void TestMethod_NewDoubleToken_IntString()
        {
            string intString = "123";

            var token = new DoubleToken(intString);

            Assert.AreEqual(123D, token.Value);
        }

        [TestMethod]
        public void TestMethod_NewDoubleToken_DoubleString()
        {
            string doubleString = "123.456";

            var token = new DoubleToken(doubleString);

            Assert.AreEqual(123.456, token.Value);

        }

        [TestMethod]
        public void TestMethod_NewDoubleToken_DoubleStringGetFromMiddleString()
        {
            string doubleString = "123.45.6";

            var token = new DoubleToken(doubleString,4);

            Assert.AreEqual(45.6, token.Value);

        }
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TestMethod_NewDoubleToken_DoubleStringWithError()
        {
            string doubleString = ".123.456";

            var token = new DoubleToken(doubleString);

        }

    }
}
