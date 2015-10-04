using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MathParser.Tokens
{
    internal class FunctionToken : OperationToken
    {
        private string source;
        private int position;

        public FunctionToken(string source, ref int endPosition)
        {
            // TODO: Complete member initialization
            this.source = source;
        }

        public override int Priority
        {
            get { return OperationToken.HighPriority; }
        }

        protected MethodInfo GetMethod() { throw new NotImplementedException(); }
        public override int Arity
        {
            get { return GetMethod().GetParameters().Length; }
        }

        public static bool IsStartSymbol(char symbol)
        {
            return Char.IsLetter(symbol);
        }

        public override Expression GetExpression(params Expression[] parameter)
        {
            if (parameter.Length != Arity)
            {
                throw new ArgumentException(string.Format("Wrong count parameters: passed - {0}, should be - {1}", parameter.Length, Arity));
            }

            return Expression.Call(GetMethod(), parameter);
        }
    }
}
