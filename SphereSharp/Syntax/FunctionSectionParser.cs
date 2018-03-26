using Sprache;
using System.Linq;

namespace SphereSharp.Syntax
{
    internal static class FunctionSectionParser
    {
        public static Parser<SectionSyntax> ParseFunction(string sectionType, string sectionName) =>
            from codeBlock in CodeBlockParser.CodeBlock
            select new FunctionSectionSyntax(sectionType, sectionName, null, codeBlock);

    }
}
