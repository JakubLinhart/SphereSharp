using System.Collections.Generic;

namespace SphereSharp.Syntax
{
    public sealed class MacroSegmentSyntax : SegmentSyntax
    {
        public MacroSyntax Macro { get; }

        public MacroSegmentSyntax(MacroSyntax macro)
        {
            Macro = macro;
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitMacroSegment(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield return Macro;
        }
    }
}
