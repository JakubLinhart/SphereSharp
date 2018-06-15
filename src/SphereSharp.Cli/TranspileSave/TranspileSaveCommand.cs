using SphereSharp.Sphere99.Sphere56Transpiler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Cli.TranspileSave
{
    public sealed class TranspileSaveCommand
    {
        private Compilation compilation = new Compilation();

        public void Transpile(TranspileSaveOptions options)
        {
            if (string.IsNullOrEmpty(options.OutputPath))
                throw new CommandLineException("No output path specified.");

            if (!Directory.Exists(options.OutputPath))
                throw new CommandLineException($"Output path {options.OutputPath} doesn't exist.");

            if (string.IsNullOrEmpty(options.CharsFileName) && string.IsNullOrEmpty(options.WorldFileName))
                throw new CommandLineException("No input file specified. Please, specify at least one of --world or --chars options.");

            if (!string.IsNullOrEmpty(options.CharsFileName))
            {
                if (!File.Exists(options.CharsFileName))
                    throw new CommandLineException($"Input file {options.CharsFileName} doesn't exist.");

                TranspileCharsFile(options.CharsFileName, options);
            }

            if (!string.IsNullOrEmpty(options.WorldFileName))
            {
                if (!File.Exists(options.WorldFileName))
                    throw new CommandLineException($"Input file {options.WorldFileName} doesn't exist.");

                TranspileWorldFile(options.WorldFileName, options);
            }
        }

        private void TranspileWorldFile(string worldFileName, TranspileSaveOptions options)
        {
            Console.WriteLine($"Parsing {worldFileName}");
            string src = File.ReadAllText(worldFileName);

            compilation.AddWorldSaveFile(worldFileName, src);
            CheckCompilationErrors();

            var outputWorldFileName = GetOutputFileName(Path.GetFileName(worldFileName), options);
            var outputDataFileName = GetOutputFileName("spheredata.scp", options);
            Console.WriteLine($"Transpiling to {outputWorldFileName}");
            Console.WriteLine($"Transpiling to {outputDataFileName}");
            var transpiler = new WorldTranspiler(compilation.DefinitionRepository);

            WorldTranspilationResult result = transpiler.Transpile(compilation.CompiledWorldSaveFile.ParsedTree);

            File.WriteAllText(outputWorldFileName, result.World);
            File.WriteAllText(outputDataFileName, result.Data);
        }

        private void TranspileCharsFile(string sphereCharsFileName, TranspileSaveOptions options)
        {
            Console.WriteLine($"Parsing {sphereCharsFileName}");
            string src = File.ReadAllText(sphereCharsFileName);

            compilation.AddCharSaveFile(sphereCharsFileName, src);
            CheckCompilationErrors();

            var outputFileName = GetOutputFileName(Path.GetFileName(sphereCharsFileName), options);
            Console.WriteLine($"Transpiling to {outputFileName}");
            var transpiler = new CharsTranspiler(compilation.DefinitionRepository);

            string result = transpiler.Transpile(compilation.CompiledCharSaveFile.ParsedTree);

            File.WriteAllText(outputFileName, result);
        }

        private void CheckCompilationErrors()
        {
            if (compilation.CompilationErrors.Any())
            {
                foreach (var error in compilation.CompilationErrors)
                {
                    if (!string.IsNullOrEmpty(error.FileName))
                        Console.WriteLine(error.FileName);
                    Console.WriteLine(error.Message);
                }

                throw new CommandLineException("Parser errors found. Transpilation terminated.");
            }
        }

        private string GetOutputFileName(string fileName, TranspileSaveOptions options)
            => Path.Combine(options.OutputPath, fileName);

    }
}
