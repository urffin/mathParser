using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParser.Tokens
{
    internal abstract class BracketToken : OperationToken
    {

        public override int Priority { get { return 0; } }

        public override System.Linq.Expressions.Expression GetExpression(params System.Linq.Expressions.Expression[] parameter)
        {
            throw new NotImplementedException();
        }

        public override int Arity { get { throw new NotImplementedException(); } }

        public static bool IsStartSymbol(char symbol)
        {
            return symbol == '(' || symbol == ')';
        }
        public static BracketToken GetBracket(char symbol, Token prevToken = null)
        {
            switch (symbol)
            {
                case '(': return new OpenBracket(prevToken);
                case ')': return new CloseBracket(prevToken);
                default: throw new ArgumentOutOfRangeException("symbol", symbol, string.Format("Have not this bracket: '{0}'", symbol));
            }
        }

        #region built-in brackets
        private class OpenBracket : BracketToken
        {
            public OpenBracket(Token prevToken)
            {
                if (prevToken != null && (prevToken.TokenType != TokenType.Operation && prevToken.TokenType != TokenType.OpenBracket))
                {
                    throw new InvalidOperationException("Open bracket can't follow after: "+prevToken.TokenType);
                }
            }

            public override TokenType TokenType
            {
                get { return TokenType.OpenBracket; }
            }
        }
        private class CloseBracket : BracketToken
        {
            public CloseBracket(Token prevToken)
            {
                if (prevToken == null)
                    throw new InvalidOperationException("Close bracket be first in expression.");

                if (prevToken.TokenType != TokenType.Value && prevToken.TokenType != TokenType.CloseBracket)
                {
                    throw new InvalidOperationException("Close bracket can't follow after: " + prevToken.TokenType);
                }

            }

            public override TokenType TokenType
            {
                get { return Tokens.TokenType.CloseBracket; }
            }
        }
        #endregion


    }
}
