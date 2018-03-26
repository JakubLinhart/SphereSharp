using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Model
{
    public sealed class ProfessionDef
    {
        public int Id { get; }
        public string DefName { get; }
        public string Name { get; }

        public ProfessionDef(int id, string defName, string name)
        {
            Id = id;
            DefName = defName;
            Name = name;
        }
    }
}
