using System.Collections.Generic;
using Sprache;

namespace SphereSharp.Syntax
{
    public class PropertySyntax : SyntaxNode
    {
        public string LValue { get; }
        public string RValue { get; }

        public PropertySyntax(string lValue, string rValue)
        {
            this.LValue = lValue;
            this.RValue = rValue;
        }

        public static PropertySyntax Parse(string src) =>
            PropertyParser.Property.Parse(src);

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitProperty(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield break;
        }
    }
}
