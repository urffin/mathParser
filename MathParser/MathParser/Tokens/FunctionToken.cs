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
        private static Dictionary<string, MethodInfo> builtInFunctions;

        static FunctionToken()
        {
            builtInFunctions = new[]{
                "Sin", "Cos", "Exp"
            }.ToDictionary(el => el, el => typeof(Math).GetMethod(el), StringComparer.InvariantCultureIgnoreCase);
        }

        private MethodInfo functionMethod;


        protected override bool CheckLexem(string lexem)
        {
            return builtInFunctions.TryGetValue(lexem, out functionMethod);
        }
        public override bool AllowUnaryAfter
        {
            get { return false; }
        }
        protected override bool CheckSymbol(char symbol)
        {
            return Char.IsLetter(symbol);
        }

        public override int Priority
        {
            get { return OperationToken.HighPriority; }
        }

        public override int Arity
        {
            get { return functionMethod.GetParameters().Length; }
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

            return Expression.Call(functionMethod, parameter);
        }
    }
}
