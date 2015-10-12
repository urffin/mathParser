using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MathParser.Special;

namespace MathParser.Tokens
{
    internal abstract class Token
    {
        protected abstract bool CheckSymbol(char symbol);
        protected abstract bool CheckLexem(string lexem);
        public abstract bool AllowUnaryAfter { get; }
        internal virtual Token FromString(string source, ref int endPosition)
        {
            StringBuilder sb = new StringBuilder();

            char symbol;
            while (endPosition < source.Length)
            {
                symbol = source[endPosition];
                if (!CheckSymbol(symbol)) break;

                sb.Append(symbol);
                endPosition += 1;
            }

            if (!CheckLexem(sb.ToString()))
            {
                if (endPosition == source.Length)
                    throw new ArgumentException(string.Format("invalide lexem: '{0}'", sb.ToString()));

                throw new ArgumentException(string.Format("unexpected symbol: '{0}'", source[endPosition]));

            };
            return this;
        }


        public abstract TokenType TokenType { get; }
        internal static int GetNextToken(string source, int position, out Token token, Token prevToken = null)
        {
            char symbol = source[position];
            token = null;
            if (DoubleToken.IsStartSymbol(symbol))
            {
                token = new DoubleToken().FromString(source, ref position);
                return position;
            }

            if (BracketToken.IsStartSymbol(symbol))
            {
                token = BracketToken.GetBracket(symbol, prevToken);
                return position + 1;
            }

            if (OperationToken.TryGetOperation(symbol, prevToken, out token))
            {
                return position + 1;
            }

            if (FunctionToken.IsStartSymbol(symbol))
            {
                token = new FunctionToken().FromString(source, ref position);
                return position;
            }

            throw new ArgumentOutOfRangeException("symbol", symbol, "Unexpected symbol");
        }

        public abstract Expression GetExpression(params Expression[] parameter);
    }
}
