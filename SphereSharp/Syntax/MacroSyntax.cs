using Sprache;
using System;
using System.Collections.Generic;
using System.Text;

namespace SphereSharp.Syntax
{
    public sealed class MacroSyntax : SyntaxNode
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

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitMacro(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield return Call;
        }
    }
}
