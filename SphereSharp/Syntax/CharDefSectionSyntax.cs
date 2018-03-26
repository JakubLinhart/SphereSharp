using System;
using System.Collections.Immutable;
using System.Linq;

namespace SphereSharp.Syntax
{
    public class CharDefSectionSyntax : SectionSyntax
    {
        public ImmutableArray<PropertySyntax> Properties { get; }
        public ImmutableArray<TriggerSyntax> Triggers { get; }

        public CharDefSectionSyntax(string type, string name, ImmutableArray<PropertySyntax> properties,
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

        public int GetPropertyNumberValue(string propertyName)
        {
            var value = GetPropertyValue(propertyName);
            if (value == null || !int.TryParse(value, out int numberValue))
                return 0;

            return numberValue;
        }
    }
}
