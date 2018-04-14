using System.Collections.Generic;

namespace SphereSharp.Syntax
{
    public class TextArgumentSyntax : ArgumentSyntax
    {
        public string Text { get; }

        public TextArgumentSyntax(string text)
        {
            Text = text;
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitTextArgument(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield break;
        }
    }
}
