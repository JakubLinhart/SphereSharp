using SphereSharp.Sphere99;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SphereSharp
{
    public sealed class Compilation
    {
        private readonly DefinitionsCollector definitionsCollector;
        private readonly DefinitionsRepository repository = new DefinitionsRepository();
        private readonly List<CompiledFile> compiledFiles = new List<CompiledFile>();
        private readonly List<Error> compilationErrors = new List<Error>();

        public IEnumerable<CompiledFile> CompiledFiles => compiledFiles;
        public CompiledFile CompiledCharSaveFile { get; private set; }
        public CompiledFile CompiledWorldSaveFile { get; private set; }
        public CompiledFile CompiledAccountSaveFile { get; private set; }
        public IEnumerable<Error> CompilationErrors => compilationErrors;
        public IDefinitionsRepository DefinitionRepository => repository;

        public Compilation()
        {
            definitionsCollector = new DefinitionsCollector(repository);
        }

        public void AddFile(string inputFileName, string src)
        {
            var parser = new Parser();
            var result = parser.ParseFile(src);

            Process(inputFileName, result);

        }

        public void AddCharSaveFile(string inputFileName, string src)
        {
            var parser = new Parser();
            var result = parser.ParseSaveFile(src);

            Process(inputFileName, result);

            CompiledCharSaveFile = new CompiledFile(inputFileName, result.Tree);
        }

        public void AddWorldSaveFile(string inputFileName, string src)
        {
            var parser = new Parser();
            var result = parser.ParseSaveFile(src);

            Process(inputFileName, result);

            CompiledWorldSaveFile = new CompiledFile(inputFileName, result.Tree);
        }

        public void AddAccountsSaveFile(string inputFileName, string src)
        {
            var parser = new Parser();
            var result = parser.ParseAccountsFile(src);
            
            Process(inputFileName, result);

            CompiledAccountSaveFile = new CompiledFile(inputFileName, result.Tree);
        }

        private void Process(string inputFileName, ParsingResult result)
        {
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
}
