using Antlr4.Runtime;
using CommandLine;
using SphereSharp.Cli.AnalyzeShard;
using SphereSharp.Cli.CreateShardSettings;
using SphereSharp.Cli.Roundtrip;
using SphereSharp.Cli.Transpile;
using SphereSharp.Cli.TranspileSave;
using SphereSharp.Cli.TranspileShard;
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
            CommandLine.Parser.Default.ParseArguments<RoundtripOptions, TranspileOptions, TranspileSaveOptions, TranspileShardOptions, CreateShardSettingsOptions, AnalyzeShardOptions>(args)
                .WithParsed<RoundtripOptions>(options => new RoundtripCommand().Roundtrip(options))
                .WithParsed<TranspileOptions>(options => new TranspileCommand().Transpile(options))
                .WithParsed<TranspileSaveOptions>(options => new TranspileSaveCommand().Transpile(options))
                .WithParsed<TranspileShardOptions>(options => new TranspileShardCommand().Transpile(options))
                .WithParsed<CreateShardSettingsOptions>(options => new CreateShardSettingsCommand().Create(options))
                .WithParsed<AnalyzeShardOptions>(options => new AnalyzeShardCommand().Analyze(options));
        }
    }
}
