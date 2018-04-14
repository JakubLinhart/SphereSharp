using System.Collections.Generic;

namespace SphereSharp.Syntax
{
    public abstract class SyntaxNode
    {
        public abstract void Accept(SyntaxVisitor visitor);
        public abstract IEnumerable<SyntaxNode> GetChildNodes();
    }
}