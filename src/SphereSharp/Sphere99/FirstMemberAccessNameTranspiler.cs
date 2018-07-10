using Antlr4.Runtime.Misc;
using SphereSharp.Sphere99.Sphere56Transpiler;

namespace SphereSharp.Sphere99
{
    internal sealed class FirstMemberAccessNameTranspiler : sphereScript99BaseVisitor<bool>
    {
        private readonly SourceCodeBuilder builder;
        private readonly Sphere56TranspilerVisitor parentTranspiler;

        public FirstMemberAccessNameTranspiler(SourceCodeBuilder builder, Sphere56TranspilerVisitor parentTranspiler)
        {
            this.builder = builder;
            this.parentTranspiler = parentTranspiler;
        }

        public override bool VisitCustomMemberAccess([NotNull] sphereScript99Parser.CustomMemberAccessContext context)
        {
            parentTranspiler.Visit(context.memberName());

            return true;
        }

        public override bool VisitNativeMemberAccess([NotNull] sphereScript99Parser.NativeMemberAccessContext context)
        {
            parentTranspiler.Visit(context.nativeFunctionName());

            return true;
        }

        public override bool VisitEvalCall([NotNull] sphereScript99Parser.EvalCallContext context)
        {
            builder.Append(context.EVAL_FUNCTIONS().GetText());

            return true;
        }

        public override bool VisitStrictNativeMemberAccess([NotNull] sphereScript99Parser.StrictNativeMemberAccessContext context)
        {
            builder.Append(context.strictNativeFunctionName().GetText());

            return true;
        }

        public override bool VisitMacro([NotNull] sphereScript99Parser.MacroContext context)
        {
            if (context.escapedMacro()?.macroBody() != null)
                return Visit(context.escapedMacro().macroBody());
            if (context.nonEscapedMacro()?.macroBody() != null)
                return Visit(context.nonEscapedMacro().macroBody());

            return base.VisitMacro(context);
        }

        public override bool VisitSignedEvalOperand([NotNull] sphereScript99Parser.SignedEvalOperandContext context)
        {
            if (context.evalOperand() != null)
                return Visit(context.evalOperand());

            return base.VisitSignedEvalOperand(context);
        }
    }
}
