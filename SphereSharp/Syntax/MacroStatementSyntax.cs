using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Syntax
{
    public class MacroStatementSyntax : StatementSyntax
    {
        public MacroSyntax Macro { get; }

        public MacroStatementSyntax(MacroSyntax macro)
        {
            Macro = macro;
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitMacroStatement(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield return Macro;
        }
    }
}
