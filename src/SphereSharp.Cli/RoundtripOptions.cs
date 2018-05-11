using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Cli
{
    [Verb("roundtrip")]
    public class RoundtripOptions
    {
        private string outputPath;

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
