﻿using System;
using System.Collections.Generic;
using System.Globalization;
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

        bool isDecimalSeparatorSet = false;
        private readonly char DecimalSeparator = Convert.ToChar(CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator);
        protected bool CheckSymbol(char symbol, int countSelectedChars)
        {
            if (symbol != DecimalSeparator) return (symbol >= '0' && symbol <= '9');

            if (!isDecimalSeparatorSet && countSelectedChars > 0)
            {
                return isDecimalSeparatorSet = true;
            }

            return false;
        }
        public DoubleToken(string source, int position = 0)
        {
            StringBuilder sb = new StringBuilder();

            char symbol;
            while (position < source.Length)
            {
                symbol = source[position];
                if (!CheckSymbol(symbol,sb.Length)) break;

                sb.Append(symbol);
                position += 1;
            }

            if (double.TryParse(sb.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out this.value))
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
