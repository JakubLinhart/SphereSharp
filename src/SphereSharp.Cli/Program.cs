using Antlr4.Runtime;
using CommandLine;
using SphereSharp.Cli.Accounts;
using SphereSharp.Cli.AnalyzeShard;
using SphereSharp.Cli.CreateShardSettings;
using SphereSharp.Cli.Roundtrip;
using SphereSharp.Cli.Transpile;
using SphereSharp.Cli.TranspileSave;
using SphereSharp.Cli.TranspileShard;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            CommandLine.Parser.Default.ParseArguments<RoundtripOptions, TranspileOptions, TranspileSaveOptions, TranspileShardOptions, CreateShardSettingsOptions, AnalyzeShardOptions, ListAccountsOptions>(args)
                .WithParsed<RoundtripOptions>(options => ExecuteCommand(() => new RoundtripCommand().Roundtrip(options)))
                .WithParsed<TranspileOptions>(options => ExecuteCommand(() => new TranspileCommand().Transpile(options)))
                .WithParsed<TranspileSaveOptions>(options => ExecuteCommand(() => new TranspileSaveCommand().Transpile(options)))
                .WithParsed<TranspileShardOptions>(options => ExecuteCommand(() => new TranspileShardCommand().Transpile(options)))
                .WithParsed<CreateShardSettingsOptions>(options => ExecuteCommand(() => new CreateShardSettingsCommand().Create(options)))
                .WithParsed<AnalyzeShardOptions>(options => ExecuteCommand(() => new AnalyzeShardCommand().Analyze(options)))
                .WithParsed<ListAccountsOptions>(options => ExecuteCommand(() => new ListAccountsCommand().List(options)));
        }

        private static void ExecuteCommand(Action commandAction)
        {
            var watch = Stopwatch.StartNew();
            try
            {
                commandAction();
            }
            finally
            {
                watch.Stop();
                Console.WriteLine();
                Console.WriteLine($"Execution time: {watch.Elapsed}");
            }
        }
    }
}
