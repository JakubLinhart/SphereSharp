using Antlr4.Runtime.Tree;
using SphereSharp.Sphere99;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Cli
{
    public struct CompiledFile
    {
        public string FileName { get; }
        public IParseTree ParsedTree { get; }

        public CompiledFile(string fileName, IParseTree parsedTree)
        {
            FileName = fileName;
            ParsedTree = parsedTree;
        }
    }

    public sealed class Compilation
    {
        private readonly DefinitionsCollector definitionsCollector;
        private readonly DefinitionsRepository repository = new DefinitionsRepository();
        private readonly List<CompiledFile> compiledFiles = new List<CompiledFile>();
        private readonly List<Error> compilationErrors = new List<Error>();

        public IEnumerable<CompiledFile> CompiledFiles => compiledFiles;
        public IEnumerable<Error> CompilationErrors => compilationErrors;
        public IDefinitionsRepository DefinitionRepository => repository;

        public Compilation()
        {
            definitionsCollector = new DefinitionsCollector(repository);
        }

        public void AddFile(string inputFileName)
        {
            var parser = new Parser();
            var result = parser.ParseFile(File.ReadAllText(inputFileName));
            if (result.Errors.Any())
            {
                compilationErrors.AddRange(result.Errors);
            }
            else
            {
                definitionsCollector.Visit(result.Tree);
                compiledFiles.Add(new CompiledFile(inputFileName, result.Tree));
            }
        }
    }

    internal sealed class TranspileCommand
    {
        private Compilation compilation = new Compilation();

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
            compilation.AddFile(inputFileName);
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
