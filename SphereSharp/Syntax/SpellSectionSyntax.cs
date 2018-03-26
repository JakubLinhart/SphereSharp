using System;
using System.Collections.Immutable;
using System.Linq;

namespace SphereSharp.Syntax
{
    public class SpellSectionSyntax : SectionSyntax
    {
        public ImmutableArray<PropertySyntax> Properties { get; }

        public int Id { get; }

        public SpellSectionSyntax(string type, string name, ImmutableArray<PropertySyntax> properties)
            : base(type, name, null)
        {
            Properties = properties;
            Id = int.Parse(name);
        }

        public string GetSinglePropertyValue(string propertyName)
        {
            return Properties.SingleOrDefault(x => x.LValue.Equals(propertyName, StringComparison.OrdinalIgnoreCase))?.RValue;
        }
    }
}
