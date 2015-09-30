﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MathParser.Tokens
{
    internal abstract class Token
    {
        protected int endPosition;
        public abstract TokenType TokenType { get; }
        internal static int GetNextToken(string source, int position, out Token token, Token prevToken = null)
        {
            char symbol = source[position];
            token = null;
            if (DoubleToken.IsStartSymbol(symbol))
            {
                token = new DoubleToken(source, position);
            }
            else if (BracketToken.IsStartSymbol(symbol))
            {
                token = BracketToken.GetBracket(symbol, prevToken);
                token.endPosition = position + 1;
            }
            else if (OperationToken.IsStartSymbol(symbol))
            {
                token = OperationToken.GetOperation(symbol, prevToken);
                token.endPosition = position + 1;
            }


            if (token != null)
                return token.endPosition;

            throw new ArgumentOutOfRangeException("symbol", symbol, "Unexpected symbol");
        }

        public abstract Expression GetExpression(params Expression[] parameter);
    }
}
