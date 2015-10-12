using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParser.Tokens
{
    internal abstract class OperationToken : Token
    {
        protected const int LowestPriority = 0;
        protected const int LowPriority = 1;
        protected const int MidPriority = 10;
        protected const int HighPriority = 20;

        public static readonly OperationToken UnaryMinus = new OperationTokenUnary.MinusOperationToken();
        public static readonly OperationToken UnaryPlus = new OperationTokenUnary.PlusOperationToken();

        private static readonly OperationToken BinaryMinus = new OperationTokenBinary.MinusOperationToken();
        private static readonly OperationToken BinaryPlus = new OperationTokenBinary.PlusOperationToken();
        private static readonly OperationToken BinaryMultiply = new OperationTokenBinary.MultiplyOperationToken();
        private static readonly OperationToken BinaryDivision = new OperationTokenBinary.DivisionOperationToken();

        protected static readonly Dictionary<char, Token> UnarySpecialChars = new Dictionary<char, Token> { 
            {'-', OperationToken.UnaryMinus},
            {'+', OperationToken.UnaryPlus}
        };
        protected static readonly Dictionary<char, Token> SpecialChars = new Dictionary<char, Token> { 
            {'-', OperationToken.BinaryMinus},
            {'+', OperationToken.BinaryPlus},
            {'/', OperationToken.BinaryDivision},
            {'*', OperationToken.BinaryMultiply}, 
        //    '(', ')', ',' 
        };
        protected static bool TryGetUnary(char symbol, Token prevToken, out Token unaryOperation)
        {
            unaryOperation = null;
            return (prevToken == null || prevToken.AllowUnaryAfter) && UnarySpecialChars.TryGetValue(symbol, out unaryOperation);
        }

        public static bool TryGetOperation(char symbol, Token prevToken, out Token token)
        {
            return TryGetUnary(symbol, prevToken, out token) || SpecialChars.TryGetValue(symbol, out token);
        }


        static OperationToken() {
            System.Diagnostics.Debug.WriteLine("Operation static ctor");
        }
        public abstract int Priority { get; }
        public abstract int Arity { get; }
        protected override bool CheckLexem(string lexem)
        {
            throw new NotImplementedException();
        }
        protected override bool CheckSymbol(char symbol)
        {
            throw new NotImplementedException();
        }
        public override bool AllowUnaryAfter
        {
            get { return true; }
        }
        public override Tokens.TokenType TokenType { get { return TokenType.Operation; } }

    }
}
