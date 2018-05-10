using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp
{
    internal sealed class MemoryErrorListener : BaseErrorListener
    {
        private List<Error> errors = new List<Error>();

        public Error[] Errors => errors.ToArray();

        public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            errors.Add(new Error(msg, line, charPositionInLine));

            base.SyntaxError(recognizer, offendingSymbol, line, charPositionInLine, msg, e);
        }
    }
}
