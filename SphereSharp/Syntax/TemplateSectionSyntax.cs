using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace SphereSharp.Syntax
{
    public class TemplateSectionSyntax : SectionSyntax
    {
        public ImmutableArray<PropertySyntax> Properties { get; }

        public TemplateSectionSyntax(string type, string name, ImmutableArray<PropertySyntax> properties)
            : base(type, name, null)
        {
            Properties = properties;
        }

        public string GetPropertyValue(string propertyName)
        {
            return Properties.FirstOrDefault(x => x.LValue.Equals(propertyName, StringComparison.OrdinalIgnoreCase))?.RValue;
        }
    }
}
