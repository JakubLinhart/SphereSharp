using System.Collections.Generic;

namespace SphereSharp.Syntax
{
    public class SymbolArgumentSyntax : ArgumentSyntax
    {
        public SymbolSyntax Symbol { get; }

        public SymbolArgumentSyntax(SymbolSyntax symbol)
        {
            Symbol = symbol;
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitSymbolArgument(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield return Symbol;
        }
    }
}
