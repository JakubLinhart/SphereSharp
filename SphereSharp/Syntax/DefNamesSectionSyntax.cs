using System.Collections.Generic;
using System.Collections.Immutable;

namespace SphereSharp.Syntax
{
    public sealed class DefNamesSectionSyntax : SectionSyntax
    {
        public ImmutableArray<DefNameSyntax> DefNames { get; }

        public DefNamesSectionSyntax(string sectionType, string sectionName, ImmutableArray<DefNameSyntax> defNames)
            : base(sectionType, sectionName, null)
        {
            this.DefNames = defNames;
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitDefNamesSection(this);

        public override IEnumerable<SyntaxNode> GetChildNodes() => DefNames;
    }
}
