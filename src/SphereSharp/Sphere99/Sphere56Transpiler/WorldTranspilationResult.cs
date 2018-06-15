using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Sphere99.Sphere56Transpiler
{
    public struct WorldTranspilationResult
    {
        public string World { get; }
        public string Data { get; }

        public WorldTranspilationResult(string world, string data)
        {
            World = world;
            Data = data;
        }
    }
}
