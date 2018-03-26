using Sprache;

namespace SphereSharp.Syntax
{
    internal static class DialogSectionParser
    {
        public static Parser<SectionSyntax> ParseDialog(string sectionType, string sectionName) =>
            from codeBlock in CodeBlockParser.CodeBlock
            select new DialogSectionSyntax(sectionType, sectionName, null, codeBlock);
    }
}
