using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Sphere99
{
    public sealed class DefinitionsRepository : IDefinitionsRepository
    {
        private HashSet<string> defNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private HashSet<string> functionNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        public void DefineDefName(string name) => defNames.Add(name);
        public bool IsDefName(string name) => defNames.Contains(name) && !functionNames.Contains(name);

        public void DefineFunctionName(string name) => functionNames.Add(name);
        public bool IsFunctionName(string name) => functionNames.Contains(name);
    }
}
