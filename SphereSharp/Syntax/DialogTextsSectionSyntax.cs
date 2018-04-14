using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace SphereSharp.Syntax
{
    public sealed class DialogTextsSectionSyntax : SectionSyntax
    {
        public ImmutableArray<string> Texts { get; }

        public DialogTextsSectionSyntax(string type, string name, string subName, ImmutableArray<string> texts)
            : base(type, name, subName)
        {
            Texts = texts;
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitDialogTextsSection(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield break;
        }
    }
}
