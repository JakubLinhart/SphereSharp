using System.Collections.Generic;

namespace SphereSharp.Syntax
{
    public class EmptyArgumentSyntax : ArgumentSyntax
    {
        public override void Accept(SyntaxVisitor visitor) => visitor.VisitEmptyArgument(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield break;
        }
    }
}
