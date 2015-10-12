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
            int position = 0;
            var token = new DoubleToken().FromString(intString, ref position) as DoubleToken;

            Assert.AreEqual(123D, token.Value);
        }

        [TestMethod]
        public void TestMethod_NewDoubleToken_DoubleString()
        {
            string doubleString = "123.456";
            int position = 0;
            var token = new DoubleToken().FromString(doubleString, ref position) as DoubleToken;

            Assert.AreEqual(123.456, token.Value);

        }

        [TestMethod]
        public void TestMethod_NewDoubleToken_DoubleStringGetFromMiddleString()
        {
            string doubleString = "123.45.6";
            int position = 4;
            var token = new DoubleToken().FromString(doubleString, ref position) as DoubleToken;

            Assert.AreEqual(45.6, token.Value);

        }
        [TestMethod]
        public void TestMethod_NewDoubleToken_DoubleStringWithError()
        {
            string doubleString = ".123.456";
            int position = 0;
            Assert.IsFalse(DoubleToken.IsStartSymbol(doubleString[position]));

        }

    }
}
