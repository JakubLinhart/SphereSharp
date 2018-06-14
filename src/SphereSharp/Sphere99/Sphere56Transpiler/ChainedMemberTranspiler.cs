using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace SphereSharp.Sphere99.Sphere56Transpiler
{
    internal sealed class ChainedMemberTranspiler : sphereScript99BaseVisitor<bool>
    {
        private readonly SourceCodeBuilder builder;
        private readonly Sphere56TranspilerVisitor parentTranspiler;

        public ChainedMemberTranspiler(SourceCodeBuilder builder, Sphere56TranspilerVisitor sphere56TranspilerVisitor)
        {
            this.builder = builder;
            this.parentTranspiler = sphere56TranspilerVisitor;
        }

        public override bool VisitFirstMemberAccess([NotNull] sphereScript99Parser.FirstMemberAccessContext context)
        {
            if (context.customMemberAccess()?.chainedMemberAccess() != null)
                parentTranspiler.Visit(context.customMemberAccess()?.chainedMemberAccess());
            else if (context.genericNativeMemberAccess()?.nativeMemberAccess()?.chainedMemberAccess() != null)
                parentTranspiler.Visit(context.genericNativeMemberAccess()?.nativeMemberAccess()?.chainedMemberAccess());

            return true;
        }
    }

    internal sealed class HasChainedMemberVisitor : sphereScript99BaseVisitor<bool>
    {
        public override bool VisitFirstMemberAccess([NotNull] sphereScript99Parser.FirstMemberAccessContext context)
        {
            return context.customMemberAccess()?.chainedMemberAccess() != null
                || context.genericNativeMemberAccess()?.nativeMemberAccess()?.chainedMemberAccess() != null;
        }
    }
}
