using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParser.Lexem
{
    internal class LexemParser
    {
        private readonly static HashSet<char> specialChars = new HashSet<char>() { '+', '-', '*', '/', '(', ')', ',' };

        private string source;
        private int position;

        public LexemParser(string source)
        {
            this.position = 0;
            this.source = source;
        }

        public bool GetNextLexem(out Lexem lexem)
        {
            StringBuilder sb = new StringBuilder();

            char symbol;
            while (position < source.Length)
            {
                symbol = source[position];
                sb.Append(symbol);
                if (specialChars.Contains(symbol))
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
    }
}
