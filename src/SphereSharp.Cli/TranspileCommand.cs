using SphereSharp.Sphere99;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Cli
{
    internal sealed class TranspileCommand
    {
        private Compilation compilation = new Compilation();
        private PretranspilationReplacementScope[] replacementScopes = new[]
        {
            new PretranspilationReplacementScope("newbie_portals.scp", new PretranspilationReplacements(new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "\"dialog(D_RACEclass_classes)\"", "\"dialog D_RACEclass_classes\"" },
                { "\"dialog(D_RACEclass_races)\"", "\"dialog D_RACEclass_races\"" },
                { "\"dialog(D_raceclass_nations)\"", "\"dialog D_raceclass_nations\"" },
                { "\"dialog(D_RACEclass_stats)\"", "\"dialog D_RACEclass_stats\"" },
            }))
        };

        public void Transpile(TranspileOptions options)
        {
            if (File.Exists(options.InputPath))
            {
                ParseFile(options.InputPath);
            }
            else if (Directory.Exists(options.InputPath))
            {
                ParseDirectory(options.InputPath);
            }
            else
                throw new CommandLineException($"Cannot find {options.InputPath}");

            if (compilation.CompilationErrors.Any())
            {
                foreach (var error in compilation.CompilationErrors)
                {
                    if (!string.IsNullOrEmpty(error.FileName))
                        Console.WriteLine(error.FileName);
                    Console.WriteLine(error.Message);
                }
            }
            else
            {
                var fileNameHandler = new TranspileCommandFileNameHandler(options);
                foreach (var file in compilation.CompiledFiles)
                {
                    var outputFileName = fileNameHandler.GetOututFileNameFromInput(file.FileName);
                    var outputDirectory = Path.GetDirectoryName(outputFileName);
                    if (!Directory.Exists(outputDirectory))
                        Directory.CreateDirectory(outputDirectory);

                    Console.WriteLine($"Transpiling to {outputFileName}");
                    var transpiler = new Sphere56TranspilerVisitor(compilation.DefinitionRepository);
                    transpiler.Visit(file.ParsedTree);
                    File.WriteAllText(outputFileName, transpiler.Output);
                }
            }
        }

        private void ParseFile(string inputFileName)
        {
            Console.WriteLine($"Parsing {inputFileName}");
            string src = File.ReadAllText(inputFileName);

            src = Patch(inputFileName, src);

            compilation.AddFile(inputFileName, src);
        }

        private string Patch(string inputFileName, string src)
        {
            var fileNameWithoutPath = Path.GetFileName(inputFileName);
            var replacement = replacementScopes.FirstOrDefault(x => x.Name.Equals(fileNameWithoutPath, StringComparison.OrdinalIgnoreCase));
            if (replacement != null)
            {
                src = replacement.Apply(src);
            }

            return src;
        }

        private void ParseDirectory(string inputDirectory)
        {
            foreach (var file in Directory.GetFiles(inputDirectory, "*.scp"))
            {
                string fileName = Path.GetFileName(file);
                ParseFile(file);
            }

            foreach (var dir in Directory.GetDirectories(inputDirectory))
            {
                var dirName = Path.GetFileName(dir);
                string inputDir = Path.Combine(inputDirectory, dirName);
                ParseDirectory(inputDir);
            }
        }
    }
}
