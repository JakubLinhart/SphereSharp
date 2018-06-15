using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace SphereSharp.Sphere99.Sphere56Transpiler
{
    public sealed class CharsTranspiler : sphereScript99BaseVisitor<bool>
    {
        private readonly Sphere56TranspilerVisitor parentVisitor;
        private SourceCodeBuilder builder = new SourceCodeBuilder();

        public CharsTranspiler(IDefinitionsRepository definitionsRepository = null)
        {
            parentVisitor = new Sphere56TranspilerVisitor(definitionsRepository ?? new DefinitionsRepository(), builder);
        }

        public override bool VisitPropertyList([NotNull] sphereScript99Parser.PropertyListContext context)
        {
            return parentVisitor.Visit(context);
        }

        public override bool VisitWorldCharSectionHeader([NotNull] sphereScript99Parser.WorldCharSectionHeaderContext context)
        {
            return parentVisitor.VisitWorldCharSectionHeader(context);
        }

        public override bool VisitWorldItemSectionHeader([NotNull] sphereScript99Parser.WorldItemSectionHeaderContext context)
        {
            return parentVisitor.VisitWorldItemSectionHeader(context);
        }

        public string Transpile(IParseTree parseTree)
        {
            Visit(parseTree);

            return builder.Output;
        }
    }
}
