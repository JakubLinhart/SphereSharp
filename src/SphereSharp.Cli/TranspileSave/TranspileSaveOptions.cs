using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Cli.TranspileSave
{
    [Verb("transpile-save")]
    public sealed class TranspileSaveOptions
    {
        [Option('c', "chars", HelpText = "Name of a spherechars file.")]
        public string CharsFileName { get; set; }

        [Option('w', "world", HelpText = "Name of a sphereworld file.")]
        public string WorldFileName { get; set; }

        [Option('o', "output", Required = true, HelpText = "Output file name or directory.")]
        public string OutputPath { get; set; }
    }
}
