using SphereSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace SphereSharp.Model
{
    public class GumpDef
    {
        public string DefName { get; set; }
        public CodeBlockSyntax InitCodeBlock { get; set; }
        public ImmutableArray<string> Texts { get; set; }
        public ImmutableDictionary<string, TriggerDef> Triggers { get; set; }
    }
}

