using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParser.Tokens
{
    internal interface IExecutable
    {
        int Priority { get; }
        int Arity { get; }
    }
}
