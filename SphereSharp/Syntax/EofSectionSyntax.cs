using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Syntax
{
    public sealed class EofSectionSyntax : SectionSyntax
    {
        public EofSectionSyntax() : base("eof", string.Empty, string.Empty)
        {
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitEofSection(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield break;
        }
    }
}
