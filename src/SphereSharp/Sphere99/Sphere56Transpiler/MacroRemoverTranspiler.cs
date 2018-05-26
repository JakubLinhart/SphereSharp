using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace SphereSharp.Sphere99.Sphere56Transpiler
{
    internal sealed class MacroRemoverTranspiler : sphereScript99BaseVisitor<bool>
    {
        private readonly SourceCodeBuilder builder;
        private readonly Sphere56TranspilerVisitor transpiler;

        public MacroRemoverTranspiler(SourceCodeBuilder builder, Sphere56TranspilerVisitor transpiler)
        {
            this.builder = builder;
            this.transpiler = transpiler;
        }

        public override bool VisitNonEscapedMacro([NotNull] sphereScript99Parser.NonEscapedMacroContext context)
        {
            transpiler.Visit(context.macroBody());

            return true;
        }

        public override bool VisitEscapedMacro([NotNull] sphereScript99Parser.EscapedMacroContext context)
        {
            transpiler.Visit(context.macroBody());

            return true;
        }

        public override bool VisitFirstMemberAccess([NotNull] sphereScript99Parser.FirstMemberAccessContext context)
        {
            transpiler.Visit(context);

            return true;
        }

        public override bool VisitIndexedMemberName([NotNull] sphereScript99Parser.IndexedMemberNameContext context)
        {
            transpiler.Visit(context);

            return true;
        }
    }
}
