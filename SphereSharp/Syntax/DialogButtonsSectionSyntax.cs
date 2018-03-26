using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace SphereSharp.Syntax
{
    public sealed class DialogButtonsSectionSyntax : SectionSyntax
    {
        public ImmutableArray<TriggerSyntax> Triggers { get; }

        public DialogButtonsSectionSyntax(string type, string name, string subName, ImmutableArray<TriggerSyntax> triggers)
            : base(type, name, subName)
        {
            Triggers = triggers;
        }
    }
}
