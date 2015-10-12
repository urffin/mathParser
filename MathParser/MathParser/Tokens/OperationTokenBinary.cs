using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MathParser.Tokens
{
    internal abstract class OperationTokenBinary : OperationToken
    {
        private const int ArityBinaryOperation = 2;
        public override Expression GetExpression(params Expression[] parameter)
        {
            if (parameter.Length != Arity)
                throw new ArgumentException("Binary operation should take two parameters");

            return GetExpression(parameter[0], parameter[1]);
        }
        public abstract Expression GetExpression(Expression left, Expression right);

        public override int Arity { get { return ArityBinaryOperation; } }

        #region built-in binary operation
        internal class PlusOperationToken : OperationTokenBinary
        {

            public override Expression GetExpression(Expression left, Expression right)
            {
                return Expression.Add(left, right);
            }

            public override int Priority
            {
                get { return OperationToken.LowPriority; }
            }
        }
        internal class MinusOperationToken : OperationTokenBinary
        {
            public override Expression GetExpression(Expression left, Expression right)
            {
                return Expression.Subtract(left, right);
            }
            public override int Priority
            {
                get { return OperationToken.LowPriority; }
            }

        }
        internal class MultiplyOperationToken : OperationTokenBinary
        {
            public override Expression GetExpression(Expression left, Expression right)
            {
                return Expression.Multiply(left, right);
            }
            public override int Priority
            {
                get { return OperationToken.MidPriority; }
            }

        }
        internal class DivisionOperationToken : OperationTokenBinary
        {
            public override Expression GetExpression(Expression left, Expression right)
            {
                return Expression.Divide(left, right);
            }
            public override int Priority
            {
                get { return OperationToken.MidPriority; }
            }

        }
        #endregion

    }
}
