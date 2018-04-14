using System.Collections.Generic;

namespace SphereSharp.Syntax
{
    public sealed class ReturnSyntax : StatementSyntax
    {
        public ArgumentSyntax Argument { get; }

        public ReturnSyntax(ArgumentSyntax argument)
        {
            Argument = argument;
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitReturn(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield break;
        }
    }
}
