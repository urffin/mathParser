﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MathParser.Tokens
{
    abstract class UnaryOperationToken : Token
    {
        private static readonly Dictionary<char, Func<UnaryOperationToken>> operations = new Dictionary<char, Func<UnaryOperationToken>> {
            { '+', () => new PlusOperationToken() },
            { '-', () => new MinusOperationToken() }
        };

        public static bool IsStartSymbol(char symbol)
        {
            return operations.ContainsKey(symbol);
        }
        public abstract int Priority { get; }
        public abstract Expression GetExpression(Expression expression);
        public static UnaryOperationToken GetOperation(char operationSymbol)
        {
            Func<UnaryOperationToken> tokenGenerator;
            if (!operations.TryGetValue(operationSymbol, out tokenGenerator))
            {
                throw new ArgumentOutOfRangeException("operationSymbol", string.Format("Have not operation: '{0}'", operationSymbol));
            }

            return tokenGenerator();
        }

        private class PlusOperationToken : UnaryOperationToken
        {

            public override Expression GetExpression(Expression expression)
            {
                return Expression.UnaryPlus(expression);
            }

            public override int Priority
            {
                get { return 20; }
            }
        }
        private class MinusOperationToken : UnaryOperationToken
        {
            public override Expression GetExpression(Expression expression)
            {
                return Expression.Negate(expression);
            }
            public override int Priority
            {
                get { return 20; }
            }

        }
    }

}
