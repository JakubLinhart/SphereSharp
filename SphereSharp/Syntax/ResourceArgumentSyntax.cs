using System.Collections.Generic;

namespace SphereSharp.Syntax
{
    public sealed class ResourceArgumentSyntax : ArgumentSyntax
    {
        public ExpressionSyntax Amount { get; }
        public string Name { get; }

        public ResourceArgumentSyntax(ExpressionSyntax amount, string name)
        {
            Amount = amount;
            Name = name;
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitResourceArgument(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield return Amount;
        }
    }
}
