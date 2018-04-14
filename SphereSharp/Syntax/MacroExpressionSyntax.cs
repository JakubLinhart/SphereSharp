using System;
using System.Collections.Generic;
using System.Text;

namespace SphereSharp.Syntax
{
    public class MacroExpressionSyntax : ExpressionSyntax
    {
        public MacroSyntax Macro { get; }

        public MacroExpressionSyntax(MacroSyntax macro)
        {
            Macro = macro;
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.AcceptEvalMacroExpression(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield return Macro;
        }
    }
}
