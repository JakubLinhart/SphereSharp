using System.Collections.Generic;

namespace SphereSharp.Syntax
{
    public sealed class MacroArgumentSyntax : ArgumentSyntax
    {
        public MacroSyntax Macro { get; }

        public MacroArgumentSyntax(MacroSyntax macro)
        {
            Macro = macro;
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitMacroArgument(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield return Macro;
        }
    }
}
