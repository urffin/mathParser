using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MathParser.Tokens
{
    internal abstract class OperationTokenUnary : OperationToken
    {
        private const int ArityUnaryOperation = 1;

        public abstract Expression GetExpression(Expression expression);
        public override Expression GetExpression(params Expression[] parameter)
        {
            if (parameter.Length != Arity)
                throw new ArgumentException("Unary operation should take one parameter");

            return GetExpression(parameter[0]);
        }

        public override int Arity { get { return ArityUnaryOperation; } }

        #region built-in unary operation
        internal class PlusOperationToken : OperationTokenUnary
        {

            public override Expression GetExpression(Expression expression)
            {
                return Expression.UnaryPlus(expression);
            }

            public override int Priority
            {
                get { return OperationToken.HighPriority; }
            }
        }
        internal class MinusOperationToken : OperationTokenUnary
        {
            public override Expression GetExpression(Expression expression)
            {
                return Expression.Negate(expression);
            }
            public override int Priority
            {
                get { return OperationToken.HighPriority; }
            }

        }
        #endregion
    }

}
