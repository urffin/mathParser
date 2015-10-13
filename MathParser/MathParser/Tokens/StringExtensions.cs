using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParser.Tokens
{
    internal static class StringExtensions
    {
        internal static IEnumerable<Token> Tokinize(this string source, int position=0){
            Token token=null;
            while (position < source.Length)
            {
                position = Token.GetNextToken(source, position, out token, token);
                yield return token;
                for (; position < source.Length && char.IsWhiteSpace(source[position]); position++) ;
            }
        }

        internal static IEnumerable<string> ToLexem(this string source){
            return null;
            int endPosition = 0;
            StringBuilder sb = new StringBuilder();

            char symbol;
            while (endPosition < source.Length)
            {
                symbol = source[endPosition];
                //if (!CheckSymbol(symbol)) break;

                sb.Append(symbol);
                endPosition += 1;
            }
        }

    }
}
