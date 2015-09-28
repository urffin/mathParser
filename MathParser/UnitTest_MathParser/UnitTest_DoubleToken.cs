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
            string doubleString = "123";

            var token = new DoubleToken(doubleString);

            Assert.AreEqual(123D, token.Value);
        }

    }
}
