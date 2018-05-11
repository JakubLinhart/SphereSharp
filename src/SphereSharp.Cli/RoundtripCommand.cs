using Antlr4.Runtime;
using SphereSharp.Sphere99;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Cli
{
    public class RoundtripCommand
    {
        public void Roundtrip(RoundtripOptions options)
        {
            if (File.Exists(options.InputPath))
            {
                string outputPath = options.OutputPath;

                if (Directory.Exists(outputPath))
                {
                    outputPath = Path.Combine(outputPath, Path.GetFileName(options.InputPath));
                }
                outputPath = AddSuffix(outputPath, options.OutputSuffix);

                RoundtripFile(options.InputPath, outputPath);
                return;
            }

            if (Directory.Exists(options.InputPath))
            {
                RoundtripDirectory(options.InputPath, options.OutputPath, options.OutputSuffix);
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

        private void RoundtripFile(string inputFileName, string outputFileName)
        {
            if (inputFileName.Equals(outputFileName, StringComparison.OrdinalIgnoreCase))
                throw new CommandLineException("Cannot parse and write to the same file.");

            var inputStream = new AntlrInputStream(File.OpenRead(inputFileName));
            var lexer = new sphereScript99Lexer(inputStream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new sphereScript99Parser(tokenStream);
            parser.AddErrorListener(new ConsoleErrorListener());

            System.Console.WriteLine($"Parsing {inputFileName}");
            var file = parser.file();

            System.Console.WriteLine($"Writing {outputFileName}");
            var generator = new RoundtripGenerator();
            generator.Visit(file);

            File.WriteAllText(outputFileName, generator.Output);
        }

        private void RoundtripDirectory(string inputDirectory, string outputDirectory, string suffix)
        {
            foreach (var file in Directory.GetFiles(inputDirectory, "*.scp"))
            {
                string fileName = Path.GetFileName(file);
                string outputFile = AddSuffix(Path.Combine(outputDirectory, fileName), suffix);

                RoundtripFile(file, outputFile);
            }

            foreach (var dir in Directory.GetDirectories(inputDirectory))
            {
                var dirName = Path.GetFileName(dir);
                string inputDir = Path.Combine(inputDirectory, dirName);
                string outputDir = Path.Combine(outputDirectory, dirName);

                if (!Directory.Exists(outputDir))
                    Directory.CreateDirectory(outputDir);

                RoundtripDirectory(inputDir, outputDir, suffix);
            }
        }
    }
}
