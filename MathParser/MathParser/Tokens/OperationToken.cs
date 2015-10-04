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

        private static HashSet<char> operationsSymbols = new HashSet<char>() { 
            '+','-','*','/'
        };
        public static bool IsStartSymbol(char symbol)
        {
            return operationsSymbols.Contains(symbol);
        }
        public abstract int Priority { get; }
        public abstract int Arity { get; }

        public override Tokens.TokenType TokenType { get { return TokenType.Operation; } }

        internal static Token GetOperation(char symbol, Token prevToken)
        {
            if (prevToken == null || prevToken.TokenType == TokenType.Operation || prevToken.TokenType == TokenType.OpenBracket)
                return OperationTokenUnary.GetOperation(symbol);

            return OperationTokenBinary.GetOperation(symbol);
        }
    }
}
