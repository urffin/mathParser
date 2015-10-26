using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParser.Lexem
{
    internal class LexemParser
    {
        private readonly static Dictionary<char, IEnumerable<string>> specials = (from spec in new[] { "+", "-", "*", "/", "(", ")", ",", "**" }
                                                                     group spec by spec[0] into g
                                                                     select new KeyValuePair<char, IEnumerable<string>>(g.Key, g))
                                                                   .ToDictionary(el => el.Key, el => el.Value);

        private string source;
        private int position;

        public LexemParser(string source)
        {
            this.position = 0;
            this.source = source;
        }

        public bool GetNextLexem(out Lexem lexem)
        {

            return TryGetSpecial(out lexem) || TryGetLexem(out lexem);
        }

        private bool TryGetLexem(out Lexem lexem)
        {
            StringBuilder sb = new StringBuilder();
            char symbol;
            while (position < source.Length)
            {
                symbol = source[position];
                sb.Append(symbol);
                if (specials.ContainsKey(symbol))
                {
                    position += (sb.Length == 0) ? 1 : 0;
                    lexem = new Lexem(sb.ToString(), position);

                    return true;


                }
                position += 1;
            }
            lexem = null;
            return false;
        }

        private bool TryGetSpecial(out Lexem lexem)
        {
            throw new NotImplementedException();
        }
    }
}
