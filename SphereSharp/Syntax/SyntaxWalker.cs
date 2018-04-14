using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Syntax
{
    public class SyntaxWalker : SyntaxVisitor
    {
        public override void DefaultVisit(SyntaxNode node)
        {
            foreach (var childNode in node.GetChildNodes())
                Visit(childNode);
        }
    }
}
