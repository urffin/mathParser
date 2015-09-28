using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParser.Tokens
{
    static class StringExtensions
    {
        internal static IEnumerable<Token> Tokinize(this string source, int position=0){
            while (position < source.Length)
            {
                Token token;
                position = Token.GetNextToken(source, position, out token);
                yield return token;
                for (; position < source.Length && char.IsWhiteSpace(source[position]); position++) ;
            }
        }
    }
}
