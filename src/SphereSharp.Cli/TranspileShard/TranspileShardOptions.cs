using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Cli.TranspileShard
{
    [Verb("shard")]
    public sealed class TranspileShardOptions
    {
        [Option('o', "output", HelpText = "Output file name or directory.", Required = true)]
        public string OutputPath { get; set; }

        [Option('s', "settings-file", HelpText = "Shard settings file name.", Required = true)]
        public string SettingsFile { get; set; }
    }
}
