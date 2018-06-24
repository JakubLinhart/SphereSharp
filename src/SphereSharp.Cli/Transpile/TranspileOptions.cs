using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Cli.Transpile
{
    [Verb("transpile")]
    public class TranspileOptions
    {
        private string outputPath;

        [Option('t', "targetVersion", Required = false)]
        public string TargetVersion { get; } = "0.56";

        [Value(0, Required = true, HelpText = "Input file or directory.")]
        public string InputPath { get; set; }

        [Option('o', "output", HelpText = "Output file name or directory.")]
        public string OutputPath
        {
            get { return outputPath ?? InputPath; }
            set { outputPath = value; }
        }

        [Option('s', "suffix", HelpText = "Suffix extension for generated files.")]
        public string OutputSuffix { get; set; }
    }
}
