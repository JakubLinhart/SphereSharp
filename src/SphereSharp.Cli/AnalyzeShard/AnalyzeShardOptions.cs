using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Cli.AnalyzeShard
{
    [Verb("analyze")]
    public sealed class AnalyzeShardOptions
    {
        [Option('s', "settings-file", HelpText = "Shard settings file name.", Required = true)]
        public string SettingsFile { get; set; }
    }
}
