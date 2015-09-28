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
        public void TestMethod_Tokinize_ExceptionWtihSpaceInsideToken()
        {
            string testString = "123 .3 + 46.23";

            var tokinize = testString.Tokinize();

            Assert.AreEqual(3, tokinize.ToArray().Length);
        }
    }
}
