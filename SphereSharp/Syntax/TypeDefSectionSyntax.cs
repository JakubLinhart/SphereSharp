using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace SphereSharp.Syntax
{
    public class TypeDefSectionSyntax : SectionSyntax
    {
        public ImmutableArray<TriggerSyntax> Triggers { get; }

        public TypeDefSectionSyntax(string type, string name, IEnumerable<TriggerSyntax> triggers)
            : base(type, name, null)
        {
            Triggers = triggers.ToImmutableArray();
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitTypeDefSection(this);

        public override IEnumerable<SyntaxNode> GetChildNodes() => Triggers;
    }
}
