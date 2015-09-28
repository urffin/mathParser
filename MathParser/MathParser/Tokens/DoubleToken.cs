using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MathParser.Tokens
{
    class DoubleToken
    {
        double value;

        int endPosition;

        protected bool CheckSymbol(char symbol)
        {
            return true;
        }
        public DoubleToken(string source, int position = 0)
        {
            StringBuilder sb = new StringBuilder();

            char symbol;
            while (position < source.Length)
            {
                symbol = source[position];
                if (!CheckSymbol(symbol)) break;

                sb.Append(symbol);
                position += 1;   
            }

            if (double.TryParse(sb.ToString(), out this.value))
            {
                endPosition = position;
            }
            else
            {
                if (position == source.Length)
                    throw new ArgumentException(string.Format("invalide double: '{0}'", sb.ToString()));

                throw new ArgumentException(string.Format("unexpected symbol: '{0}'", source[position]));
            }

        }

        public double Value { get { return value; } }
    }
}
