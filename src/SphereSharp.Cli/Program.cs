using Antlr4.Runtime;
using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<RoundtripOptions, TranspileOptions>(args)
                .WithParsed<RoundtripOptions>(options => new RoundtripCommand().Roundtrip(options))
                .WithParsed<TranspileOptions>(options => new TranspileCommand().Transpile(options));
        }
    }
}
