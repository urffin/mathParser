using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParser.Lexem
{
    internal class Lexem
    {
        public int EndPosition { get; private set; }
        public string Lex { get; private set; }

        public Lexem(string lex, int endPosition)
        {
            EndPosition = endPosition;
            Lex = lex;
        }
    }
}
