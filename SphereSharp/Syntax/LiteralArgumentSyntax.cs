using System.Collections.Generic;

namespace SphereSharp.Syntax
{
    public sealed class LiteralArgumentSyntax : ArgumentSyntax
    {
        public LiteralSyntax Literal { get;  }

        public LiteralArgumentSyntax(LiteralSyntax literal)
        {
            Literal = literal;
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitLiteralArgument(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield return Literal;
        }
    }
}
