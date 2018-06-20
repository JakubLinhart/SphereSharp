using Antlr4.Runtime.Misc;

namespace SphereSharp.Sphere99
{
    internal class ChainedMemberAccessNameVisitor : sphereScript99BaseVisitor<string>
    {
        private int targetLevel;
        private int currentLevel = 0;

        public ChainedMemberAccessNameVisitor(int level)
        {
            targetLevel = level;
        }

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

        public override string VisitChainedMemberAccess([NotNull] sphereScript99Parser.ChainedMemberAccessContext context)
        {
            currentLevel++;

            return base.VisitChainedMemberAccess(context);
        }

        public override string VisitCustomMemberAccess([NotNull] sphereScript99Parser.CustomMemberAccessContext context)
        {
            if (currentLevel == targetLevel || (targetLevel == -1 && context.chainedMemberAccess() == null))
                return context.memberName().GetText();

            return base.VisitCustomMemberAccess(context);
        }

        public override string VisitNativeMemberAccess([NotNull] sphereScript99Parser.NativeMemberAccessContext context)
        {
            if (currentLevel == targetLevel || (targetLevel == -1 && context.chainedMemberAccess() == null))
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
