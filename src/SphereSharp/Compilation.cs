using SphereSharp.Sphere99;
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
}
