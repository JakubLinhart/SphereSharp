﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace SphereSharp.Syntax
{
    public class ProfessionSectionSyntax : SectionSyntax
    {
        public ImmutableArray<PropertySyntax> Properties { get; }
        public ImmutableArray<TriggerSyntax> Triggers { get; }

        public int Id { get; }

        public ProfessionSectionSyntax(string type, string name, ImmutableArray<PropertySyntax> properties,
            ImmutableArray<TriggerSyntax> triggers)
            : base(type, name, null)
        {
            Properties = properties;
            Triggers = triggers;
            Id = int.Parse(name);
        }

        public string GetSinglePropertyValue(string propertyName)
        {
            return Properties.SingleOrDefault(x => x.LValue.Equals(propertyName, StringComparison.OrdinalIgnoreCase))?.RValue;
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitProfessionSection(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            foreach (var property in Properties)
                yield return property;
            foreach (var trigger in Triggers)
                yield return trigger;
        }
    }
}
