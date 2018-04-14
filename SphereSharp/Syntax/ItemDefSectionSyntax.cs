using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace SphereSharp.Syntax
{
    public class ItemDefSectionSyntax : SectionSyntax
    {
        public ImmutableArray<PropertySyntax> Properties { get; }
        public ImmutableArray<TriggerSyntax> Triggers { get; }

        public ItemDefSectionSyntax(string type, string name, ImmutableArray<PropertySyntax> properties,
            ImmutableArray<TriggerSyntax> triggers)
            : base(type, name, null)
        {
            Properties = properties;
            Triggers = triggers;
        }

        public string GetPropertyValue(string propertyName)
        {
            return Properties.FirstOrDefault(x => x.LValue.Equals(propertyName, StringComparison.OrdinalIgnoreCase))?.RValue;
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitItemDefSection(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            foreach (var property in Properties)
                yield return property;
            foreach (var trigger in Triggers)
                yield return trigger;
        }
    }
}
