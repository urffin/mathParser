using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MathParser.Tokens
{
    abstract class BinaryOperationToken : Token
    {
        private static readonly Dictionary<char, Func<BinaryOperationToken>> operations = new Dictionary<char, Func<BinaryOperationToken>> {
            { '+', ()=>new PlusOperationToken() },
            { '-', () => new MinusOperationToken() },
            { '*', () => new MultiplyOperationToken() },
            { '/', () => new DivisionOperationToken() }
        };

        public static bool IsStartSymbol(char symbol)
        {
            return operations.ContainsKey(symbol);
        }
        public abstract int Priority { get; }

        private class PlusOperationToken : BinaryOperationToken
        {

            public override Expression GetExpression(Expression left, Expression right)
            {
                return Expression.Add(left, right);
            }

            public override int Priority
            {
                get { return 1; }
            }
        }
        private class MinusOperationToken : BinaryOperationToken
        {
            public override Expression GetExpression(Expression left, Expression right)
            {
                return Expression.Subtract(left, right);
            }
            public override int Priority
            {
                get { return 1; }
            }

        }
        private class MultiplyOperationToken : BinaryOperationToken
        {
            public override Expression GetExpression(Expression left, Expression right)
            {
                return Expression.Multiply(left, right);
            }
            public override int Priority
            {
                get { return 10; }
            }

        }
        private class DivisionOperationToken : BinaryOperationToken
        {
            public override Expression GetExpression(Expression left, Expression right)
            {
                return Expression.Divide(left, right);
            }
            public override int Priority
            {
                get { return 10; }
            }

        }

        public static BinaryOperationToken GetOperation(char operationSymbol)
        {
            Func<BinaryOperationToken> tokenGenerator;
            if (!operations.TryGetValue(operationSymbol, out tokenGenerator))
            {
                throw new ArgumentOutOfRangeException("operationSymbol", string.Format("Have not operation: '{0}'", operationSymbol));
            }

            return tokenGenerator();
        }

        public abstract Expression GetExpression(Expression left, Expression right);
    }
}
