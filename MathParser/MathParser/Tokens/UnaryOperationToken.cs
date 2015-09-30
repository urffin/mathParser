using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MathParser.Tokens
{
    internal abstract class UnaryOperationToken : OperationToken
    {
        private const int ArityUnaryOperation = 1;
        private static readonly Dictionary<char, Func<UnaryOperationToken>> operations = new Dictionary<char, Func<UnaryOperationToken>> {
            { '+', () => new PlusOperationToken() },
            { '-', () => new MinusOperationToken() }
        };
        public static UnaryOperationToken GetOperation(char operationSymbol)
        {
            Func<UnaryOperationToken> tokenGenerator;
            if (!operations.TryGetValue(operationSymbol, out tokenGenerator))
            {
                throw new ArgumentOutOfRangeException("operationSymbol", string.Format("Have not operation: '{0}'", operationSymbol));
            }

            return tokenGenerator();
        }

        public abstract Expression GetExpression(Expression expression);
        public override Expression GetExpression(params Expression[] parameter)
        {
            if (parameter.Length != Arity)
                throw new ArgumentException("Unary operation should take one parameter");

            return GetExpression(parameter[0]);
        }

        public override int Arity { get { return ArityUnaryOperation; } }

        #region built-in unary operation
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
        #endregion
    }

}
