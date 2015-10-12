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
        protected override bool CheckSymbol(char symbol)
        {
            if (symbol != DecimalSeparator) return (symbol >= '0' && symbol <= '9');

            if (!isDecimalSeparatorSet) return isDecimalSeparatorSet = true;

            return false;
        }
        public override bool AllowUnaryAfter
        {
            get { return false; }
        }
        public static bool IsStartSymbol(char symbol)
        {
            return (symbol >= '0' && symbol <= '9');
        }

        protected override bool CheckLexem(string lexem)
        {
            return double.TryParse(lexem, NumberStyles.Float, CultureInfo.InvariantCulture, out this.value);
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
