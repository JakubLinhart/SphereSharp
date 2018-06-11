using Antlr4.Runtime.Misc;

namespace SphereSharp.Sphere99
{
    internal sealed class FinalChainedMemberAccessNameVisitor : sphereScript99BaseVisitor<string>
    {
        public override string VisitStatement([NotNull] sphereScript99Parser.StatementContext context)
        {
            if (context.call() != null)
                return Visit(context.call());

            return base.VisitStatement(context);
        }

        public override string VisitCall([NotNull] sphereScript99Parser.CallContext context)
        {
            return Visit(context.firstMemberAccess());
        }

        public override string VisitCustomMemberAccess([NotNull] sphereScript99Parser.CustomMemberAccessContext context)
        {
            if (context.chainedMemberAccess() == null)
                return context.memberName().GetText();

            return base.VisitCustomMemberAccess(context);
        }

        public override string VisitNativeMemberAccess([NotNull] sphereScript99Parser.NativeMemberAccessContext context)
        {
            if (context.chainedMemberAccess() == null)
                return context.nativeFunctionName().GetText();

            return base.VisitNativeMemberAccess(context);
        }

        public override string VisitEvalCall([NotNull] sphereScript99Parser.EvalCallContext context)
        {
            return context.EVAL_FUNCTIONS().GetText();
        }

        public override string VisitActionMemberAccess([NotNull] sphereScript99Parser.ActionMemberAccessContext context)
        {
            return context.ACTION().GetText();
        }
    }
}
