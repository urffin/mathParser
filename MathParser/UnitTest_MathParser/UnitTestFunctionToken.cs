using System;
using MathParser.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest_MathParser
{
    [TestClass]
    public class UnitTestFunctionToken
    {
        [TestMethod]
        public void TestMethod_NewFunctionToken()
        {
            string function = "exp";
            int position = 0;
            var token = new FunctionToken(function, ref position);

            Assert.AreEqual(1, token.Arity, "Arity for 'exp' function should be '1'");
        }
    }
}
