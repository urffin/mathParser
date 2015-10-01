using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathParser;
using System.Diagnostics;

namespace UnitTest_MathParser
{
    [TestClass]
    public class UnitTest_Parser
    {
        [TestMethod]
        public void TestMethod_Parser_ParseFuncWithoutParams_UnaryPlus()
        {
            string expression = "+2";
            var parsedExpression = Parser.ParseWithoutParameter(expression);
            Debug.WriteLine(parsedExpression);

            Func<double> result = parsedExpression.Compile();

            Assert.AreEqual(2, result());
        }
        [TestMethod]
        public void TestMethod_Parser_ParseFuncWithoutParams_UnaryMinus()
        {
            string expression = "-5";
            var parsedExpression = Parser.ParseWithoutParameter(expression);
            Debug.WriteLine(parsedExpression);

            Func<double> result = parsedExpression.Compile();

            Assert.AreEqual(-5, result());
        }

        [TestMethod]
        public void TestMethod_Parser_ParseFuncWithoutParams_BinaryPlus()
        {
            string expression = "1+2";
            var parsedExpression = Parser.ParseWithoutParameter(expression);
            Debug.WriteLine(parsedExpression);

            Func<double> result = parsedExpression.Compile();

            Assert.AreEqual(3, result());
        }

        [TestMethod]
        public void TestMethod_Parser_ParseFuncWithoutParams_BinaryOpeartions()
        {
            string expression = "1+2-3";
            var parsedExpression = Parser.ParseWithoutParameter(expression);
            Debug.WriteLine(parsedExpression);

            Func<double> result = parsedExpression.Compile();

            Assert.AreEqual(0, result());
        }

        [TestMethod]
        public void TestMethod_Parser_ParseFuncWithoutParams_BinaryOpeartionsWithPriority()
        {
            string expression = "1+2*3-3*2";
            var parsedExpression = Parser.ParseWithoutParameter(expression);
            Debug.WriteLine(parsedExpression);

            Func<double> result = parsedExpression.Compile();

            Assert.AreEqual(1, result());
        }

        [TestMethod]
        public void TestMethod_Parser_ParseFuncWithoutParams_BinaryOpeartionsWithUnary()
        {
            string expression = "1+2*-3-3*2";
            var parsedExpression = Parser.ParseWithoutParameter(expression);
            Debug.WriteLine(parsedExpression);

            Func<double> result = parsedExpression.Compile();

            Assert.AreEqual(-11, result());
        }

        [TestMethod]
        public void TestMethod_Parser_ParseFuncWithoutParams_WithBrackets()
        {
            string expression = "1+2*-(3-3)*2";
            var parsedExpression = Parser.ParseWithoutParameter(expression);
            Debug.WriteLine(parsedExpression);

            Func<double> result = parsedExpression.Compile();

            Assert.AreEqual(1, result());
        }

        [TestMethod,ExpectedException(typeof(InvalidOperationException))]
        public void TestMethod_Parser_ParseFuncWithoutParams_WithErrorBrackets()
        {
            string expression = "()";
            var parsedExpression = Parser.ParseWithoutParameter(expression);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void TestMethod_Parser_ParseFuncWithoutParams_NotOpenedCloseBracket()
        {
            string expression = "123)";
            var parsedExpression = Parser.ParseWithoutParameter(expression);
        }
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void TestMethod_Parser_ParseFuncWithoutParams_OpenBracketAfterValue()
        {
            string expression = "123(12-12)";
            var parsedExpression = Parser.ParseWithoutParameter(expression);
        }
    }
}
