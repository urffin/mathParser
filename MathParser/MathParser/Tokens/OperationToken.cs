using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParser.Tokens
{
    internal abstract class OperationToken : Token
    {
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
                return UnaryOperationToken.GetOperation(symbol);

            return BinaryOperationToken.GetOperation(symbol);
        }
    }
}
