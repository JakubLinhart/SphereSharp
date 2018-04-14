using System.Collections.Generic;

namespace SphereSharp.Syntax
{
    public sealed class DefNameSyntax : SyntaxNode
    {
        public string LValue { get; }
        public string RValue { get; }

        public DefNameSyntax(string lValue, string rValue)
        {
            this.LValue = lValue;
            this.RValue = rValue;
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitDefName(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield break;
        }
    }
}
