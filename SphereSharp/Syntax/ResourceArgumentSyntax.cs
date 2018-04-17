using System.Collections.Generic;

namespace SphereSharp.Syntax
{
    public sealed class ResourceArgumentSyntax : ArgumentSyntax
    {
        public ExpressionSyntax Amount { get; }
        public SymbolSyntax Name { get; }

        public ResourceArgumentSyntax(ExpressionSyntax amount, SymbolSyntax name)
        {
            Amount = amount;
            Name = name;
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitResourceArgument(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield return Amount;
            yield return Name;
        }
    }
}
