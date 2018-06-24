using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Cli
{
    public class ShardSettings
    {
        public string ScriptsPath { get; set; }
        public string SavePath { get; set; }

        public string[] IgnoredScripts { get; set; }

        public PatchSet[] PrePatches { get; set; }
    }
}
