using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathParser.Tokens;


namespace UnitTest_MathParser
{
    [TestClass]
    public class UnitTest_Tokinize
    {
        [TestMethod]
        public void TestMethod_Tokinize()
        {
            string testString = "123.3+46.23";

            var tokinize = testString.Tokinize();

            Assert.AreEqual(3, tokinize.ToArray().Length);
        }

        [TestMethod]
        public void TestMethod_Tokinize_SkipSpaceBetweenTokes()
        {
            string testString = "123.3 + 46.23";

            var tokinize = testString.Tokinize();

            Assert.AreEqual(3, tokinize.ToArray().Length);
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestMethod_Tokinize_ExceptionWithSpaceInsideToken()
        {
            string testString = "123 .3 + 46.23";

            var tokinize = testString.Tokinize();

            Assert.AreEqual(3, tokinize.ToArray().Length);
        }

        [TestMethod]
        public void TestMethod_Tokinize_GetUnaryOperation()
        {
            string expression = "-123";

            var tokinize = expression.Tokinize().ToArray();

            Assert.AreEqual(tokinize.Length, 2, "expression contains two tokens");
            Assert.IsInstanceOfType(tokinize[0], typeof(OperationTokenUnary), "first token not unary minus");
        }

        [TestMethod]
        public void TestMethod_Tokinize_GetBinaryOperation()
        {
            string expression = "12-123";

            var tokinize = expression.Tokinize().ToArray();
            Assert.AreEqual(tokinize.Length, 3, "expression contains three tokens");
            Assert.IsInstanceOfType(tokinize[1], typeof(OperationTokenBinary.MinusOperationToken), "token not binary minus");
        }

        [TestMethod]
        public void TestMethod_Tokinize_ExpressionWithBrackets()
        {
            string expression = "(123)";

            var tokinize = expression.Tokinize().ToArray();

            Assert.AreEqual(tokinize.Length, 3, "expression contains three tokens");
        }
        [TestMethod]
        public void TestMethod_Tokinize_ExpressionWithBracketsAndUnary()
        {
            string expression = "(-123)";

            var tokinize = expression.Tokinize().ToArray();

            Assert.AreEqual(tokinize.Length, 4, "expression contains four tokens");
        }

        [TestMethod]
        public void TestMethod_Tokinize_ExpressionWithBracketsAndUnexpectedUnary()
        {
            string expression = "(-123-2)";

            var tokinize = expression.Tokinize().ToArray();

            Assert.AreEqual(tokinize.Length, 6, "expression contains six tokens");
        }
    }
}
