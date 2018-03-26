using Sprache;
using System;
using System.Collections.Generic;
using System.Text;

namespace SphereSharp.Syntax
{
    public sealed class MacroSyntax
    {
        public CallSyntax Call { get; }

        public MacroSyntax(CallSyntax call)
        {
            this.Call = call;
        }

        public static MacroSyntax Parse(string src)
        {
            return MacroParser.Macro.Parse(src);
        }
    }
}
