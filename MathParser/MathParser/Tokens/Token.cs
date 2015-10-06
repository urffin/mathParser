using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MathParser.Tokens
{
    internal abstract class Token
    {



        public abstract TokenType TokenType { get; }
        internal static int GetNextToken(string source, int position, out Token token, Token prevToken = null)
        {
            char symbol = source[position];
            token = null;
            if (DoubleToken.IsStartSymbol(symbol))
            {
                token = new DoubleToken(source, ref position);
                return position;
            }
            
            if (BracketToken.IsStartSymbol(symbol))
            {
                token = BracketToken.GetBracket(symbol, prevToken);
                return position + 1;
            }

            if (OperationToken.IsStartSymbol(symbol))
            {
                token = OperationToken.GetOperation(symbol, prevToken);
                return position + 1;
            }

            if (FunctionToken.IsStartSymbol(symbol))
            {
                token = new FunctionToken(source, ref position);
                return position;
            }

            throw new ArgumentOutOfRangeException("symbol", symbol, "Unexpected symbol");
        }

        public abstract Expression GetExpression(params Expression[] parameter);
    }
}
