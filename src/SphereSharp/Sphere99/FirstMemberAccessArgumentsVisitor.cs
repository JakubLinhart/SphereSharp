using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Sphere99
{
    public abstract class MemberAccessArgumentsVisitor : sphereScript99BaseVisitor<IParseTree[]>
    {
        public override IParseTree[] VisitStatement([NotNull] sphereScript99Parser.StatementContext context)
        {
            if (context.call() != null)
                return Visit(context.call());

            return base.VisitStatement(context);
        }

        public override IParseTree[] VisitCall([NotNull] sphereScript99Parser.CallContext context)
        {
            return Visit(context.firstMemberAccess());
        }

        public override IParseTree[] VisitStrictNativeMemberAccess([NotNull] sphereScript99Parser.StrictNativeMemberAccessContext context)
        {
            if (context.strictNativeArgumentList() != null)
                return context.strictNativeArgumentList().evalExpression();
            else if (context.enclosedArgumentList()?.enclosedArgumentListInner()?.enclosedArgument() != null)
                return context.enclosedArgumentList().enclosedArgumentListInner().enclosedArgument();

            return Array.Empty<IParseTree>();
        }

        protected abstract bool CanVisitCustomMemberAccess([NotNull] sphereScript99Parser.CustomMemberAccessContext context);

        public override IParseTree[] VisitCustomMemberAccess([NotNull] sphereScript99Parser.CustomMemberAccessContext context)
        {
            if (!CanVisitCustomMemberAccess(context))
                return base.VisitCustomMemberAccess(context);

            var arguments = context.enclosedArgumentList()?.enclosedArgumentListInner()?.enclosedArgument();
            if (arguments != null && arguments.Length > 0)
                return arguments;
            else
                return Array.Empty<IParseTree>();
        }

        protected abstract bool CanVisitNatvieMemberAccess(sphereScript99Parser.NativeMemberAccessContext context);

        public override IParseTree[] VisitNativeMemberAccess([NotNull] sphereScript99Parser.NativeMemberAccessContext context)
        {
            if (!CanVisitNatvieMemberAccess(context))
                return base.VisitNativeMemberAccess(context);

            if (context.nativeArgumentList()?.enclosedArgumentList()?.enclosedArgumentListInner()?.enclosedArgument() != null)
                return context.nativeArgumentList().enclosedArgumentList().enclosedArgumentListInner().enclosedArgument().Select(x => x.children[0]).ToArray();

            var freeArgumentList = context.nativeArgumentList()?.freeArgumentList();
            if (freeArgumentList != null)
            {
                var arguments = new List<IParseTree>();
                var optionalWhiteSpaceArgument = freeArgumentList?.firstFreeArgument()?.firstFreeArgumentOptionalWhiteSpace();
                if (optionalWhiteSpaceArgument != null)
                {
                    arguments.Add(optionalWhiteSpaceArgument.evalExpression()
                        ?? optionalWhiteSpaceArgument.triggerArgument()
                        ?? (IParseTree)optionalWhiteSpaceArgument.quotedLiteralArgument());
                }

                var mandatoryWhiteSpaceArgument = freeArgumentList?.firstFreeArgument()?.firstFreeArgumentMandatoryWhiteSpace();
                if (mandatoryWhiteSpaceArgument != null)
                {
                    arguments.Add(mandatoryWhiteSpaceArgument.assignmentArgument()
                        ?? (IParseTree)mandatoryWhiteSpaceArgument.unquotedLiteralArgument());
                }

                arguments.AddRange(freeArgumentList.argument());

                return arguments.ToArray();
            }

            return Array.Empty<IParseTree>();
        }

        public override IParseTree[] VisitArgumentList([NotNull] sphereScript99Parser.ArgumentListContext context)
        {
            return context.argument();
        }
    }

    public class FinalChainedMemberAccessArgumentsVisitor : MemberAccessArgumentsVisitor
    {
        protected override bool CanVisitCustomMemberAccess([NotNull] sphereScript99Parser.CustomMemberAccessContext context) 
            => context.chainedMemberAccess() == null;

        protected override bool CanVisitNatvieMemberAccess(sphereScript99Parser.NativeMemberAccessContext context) 
            => context.chainedMemberAccess() == null;
    }

    public class FirstMemberAccessArgumentsVisitor : MemberAccessArgumentsVisitor
    {
        protected override bool CanVisitCustomMemberAccess([NotNull] sphereScript99Parser.CustomMemberAccessContext context) 
            => true;

        protected override bool CanVisitNatvieMemberAccess(sphereScript99Parser.NativeMemberAccessContext context) 
            => true;
    }
}

