using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Model
{
    public class SkillDef
    {
        public int Id { get; set; }
        public string DefName { get; set; }

        public ImmutableDictionary<string, TriggerDef> Triggers { get; set; } = ImmutableDictionary<string, TriggerDef>.Empty;
    }
}
