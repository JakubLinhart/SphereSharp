using System.Collections.Generic;

namespace SphereSharp.Syntax
{
    public class CallExpressionSyntax : ExpressionSyntax
    {
        public CallSyntax Call { get; }

        public CallExpressionSyntax(CallSyntax call)
        {
            this.Call = call;
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitCallExpression(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield return Call;
        }
    }
}