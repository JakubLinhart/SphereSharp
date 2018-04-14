using Sprache;
using System.Text;

namespace SphereSharp.Syntax
{
    public abstract class SectionSyntax : SyntaxNode
    {
        public string Type { get; }
        public string Name { get; }
        public string SubType { get; }

        protected SectionSyntax(string type, string name, string subType)
        {
            Type = type;
            Name = name;
            SubType = subType;
        }

        public static SectionSyntax Parse(string src)
            => SectionParser.Section.Parse(src);
    }
}
