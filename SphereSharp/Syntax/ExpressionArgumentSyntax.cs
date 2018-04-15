using System.Collections.Generic;

namespace SphereSharp.Syntax
{
    public sealed class ExpressionArgumentSyntax : ArgumentSyntax
    {
        public ExpressionSyntax Expression { get; }

        public ExpressionArgumentSyntax(ExpressionSyntax expression)
        {
            Expression = expression;
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitExpressionArgument(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield return Expression;
        }
    }
}
