using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace SphereSharp.Sphere99.Sphere56Transpiler
{
    internal sealed class LiteralArgumentTranspiler : sphereScript99BaseVisitor<bool>
    {
        private readonly Sphere56TranspilerVisitor parentVisitor;
        private readonly SourceCodeBuilder builder;

        private readonly bool stripDoubleQuotes = false;

        public LiteralArgumentTranspiler(Sphere56TranspilerVisitor parentVisitor, SourceCodeBuilder builder, bool stripDoubleQuotes = false)
        {
            this.parentVisitor = parentVisitor;
            this.builder = builder;
            this.stripDoubleQuotes = stripDoubleQuotes;
        }

        public override bool VisitMacro([NotNull] sphereScript99Parser.MacroContext context)
        {
            parentVisitor.Visit(context);

            return true;
        }

        public override bool VisitQuotedLiteralArgument([NotNull] sphereScript99Parser.QuotedLiteralArgumentContext context)
        {
            if (!stripDoubleQuotes)
                return base.VisitQuotedLiteralArgument(context);

            return Visit(context.innerQuotedLiteralArgument());
        }

        public override bool VisitTerminal(ITerminalNode node)
        {
            builder.Append(node.GetText());

            return true;
        }
    }
}
