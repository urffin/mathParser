using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MathParser.Tokens
{
    internal class DoubleToken : Token
    {
        double value;

        bool isDecimalSeparatorSet = false;
        private readonly char DecimalSeparator = Convert.ToChar(CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator);
        protected bool CheckSymbol(char symbol)
        {
            if (symbol != DecimalSeparator) return (symbol >= '0' && symbol <= '9');

            if (!isDecimalSeparatorSet) return isDecimalSeparatorSet = true;

            return false;
        }

        public static bool IsStartSymbol(char symbol)
        {
            return (symbol >= '0' && symbol <= '9');
        }

        public DoubleToken(string source, ref int endPosition)
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

            if (!double.TryParse(sb.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out this.value))
            {
                if (endPosition == source.Length)
                    throw new ArgumentException(string.Format("invalide double: '{0}'", sb.ToString()));

                throw new ArgumentException(string.Format("unexpected symbol: '{0}'", source[endPosition]));
            }

        }

        public double Value { get { return value; } }

        public override Expression GetExpression(params Expression[] parameter)
        {
            return Expression.Constant(value);
        }
        public override TokenType TokenType
        {
            get { return TokenType.Value; }
        }
    }
}
