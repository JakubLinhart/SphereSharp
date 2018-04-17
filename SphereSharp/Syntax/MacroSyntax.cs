using Sprache;
using System;
using System.Collections.Generic;
using System.Text;

namespace SphereSharp.Syntax
{
    public sealed class MacroSyntax : SyntaxNode
    {
        public ExpressionSyntax Expression { get; }

        public MacroSyntax(ExpressionSyntax expression)
        {
            this.Expression = expression;
        }

        public static MacroSyntax Parse(string src)
        {
            return MacroParser.Macro.Parse(src);
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitMacro(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield return Expression;
        }
    }
}
