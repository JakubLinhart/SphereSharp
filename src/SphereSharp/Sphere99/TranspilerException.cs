using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Sphere99
{

    [Serializable]
    public class TranspilerException : Exception
    {
        public TranspilerException(ParserRuleContext context, string message) 
            : base($"{context.Start.Line},{context.Start.Column}: {message}\n{context.GetText()}") { }
    }
}
