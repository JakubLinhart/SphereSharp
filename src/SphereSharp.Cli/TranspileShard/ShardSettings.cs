using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Cli.TranspileShard
{
    public class ShardSettings
    {
        public string ScriptsPath { get; set; }
        public string SavePath { get; set; }

        public string[] IgnoredScripts { get; set; }
    }
}
