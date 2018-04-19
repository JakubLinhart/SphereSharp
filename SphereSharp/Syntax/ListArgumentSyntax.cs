using System.Collections.Generic;

namespace SphereSharp.Syntax
{
    public class ListArgumentSyntax : ArgumentSyntax
    {
        public ListArgumentSyntax(ArgumentListSyntax list)
        {
            List = list;
        }

        public ArgumentListSyntax List { get; }

        public override void Accept(SyntaxVisitor visitor)
        {
            visitor.VisitListArgument(this);
        }

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield return List;
        }
    }
}
