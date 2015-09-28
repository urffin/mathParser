using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParser.Tokens
{
    abstract class Token
    {
        protected int endPosition;
        internal static int GetNextToken(string source, int position, out Token token)
        {
            char symbol = source[position];
            token = null;
            if (DoubleToken.IsStartSymbol(symbol))
            {
                token = new DoubleToken(source, position);
            }
            if (BinaryOperationToken.IsStartSymbol(symbol))
            {
                token = BinaryOperationToken.GetOperation(symbol);
                token.endPosition = position + 1;
            }


            if(token != null)
                return token.endPosition;

            throw new ArgumentOutOfRangeException("symbol", symbol, "Unexpected symbol");
        }
    }
}
