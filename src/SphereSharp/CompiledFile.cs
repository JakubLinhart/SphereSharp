using Antlr4.Runtime.Tree;

namespace SphereSharp
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
}
