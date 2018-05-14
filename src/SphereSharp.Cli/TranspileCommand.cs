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
        public void Transpile(TranspileOptions options)
        {
            if (File.Exists(options.InputPath))
            {
                string outputPath = options.OutputPath;

                if (Directory.Exists(outputPath))
                {
                    outputPath = Path.Combine(outputPath, Path.GetFileName(options.InputPath));
                }
                outputPath = AddSuffix(outputPath, options.OutputSuffix);

                TranspileFile(options.InputPath, outputPath);
                return;
            }

            if (Directory.Exists(options.InputPath))
            {
                TranspileDirectory(options.InputPath, options.OutputPath, options.OutputSuffix);
                return;
            }

            throw new CommandLineException($"Cannot find {options.InputPath}");
        }

        private string AddSuffix(string fileName, string suffix)
        {
            if (string.IsNullOrEmpty(suffix))
                return fileName;

            return fileName + "." + suffix;
        }

        private void TranspileFile(string inputFileName, string outputFileName)
        {
            if (inputFileName.Equals(outputFileName, StringComparison.OrdinalIgnoreCase))
                throw new CommandLineException("Cannot parse and write to the same file.");

            var parser = new Parser();
            var result = parser.ParseFile(File.ReadAllText(inputFileName));
            if (result.Errors.Any())
            {
                foreach (var error in result.Errors)
                    Console.WriteLine(error.Message);
            }

            var transpiler = new Sphere56Transpiler();
            transpiler.Visit(result.Tree);
            
            File.WriteAllText(outputFileName, transpiler.Output);
        }

        private void TranspileDirectory(string inputDirectory, string outputDirectory, string suffix)
        {
            foreach (var file in Directory.GetFiles(inputDirectory, "*.scp"))
            {
                string fileName = Path.GetFileName(file);
                string outputFile = AddSuffix(Path.Combine(outputDirectory, fileName), suffix);

                TranspileFile(file, outputFile);
            }

            foreach (var dir in Directory.GetDirectories(inputDirectory))
            {
                var dirName = Path.GetFileName(dir);
                string inputDir = Path.Combine(inputDirectory, dirName);
                string outputDir = Path.Combine(outputDirectory, dirName);

                if (!Directory.Exists(outputDir))
                    Directory.CreateDirectory(outputDir);

                TranspileDirectory(inputDir, outputDir, suffix);
            }
        }
    }
}
