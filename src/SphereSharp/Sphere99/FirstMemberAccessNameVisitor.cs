using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace SphereSharp.Sphere99
{
    internal sealed class FirstMemberAccessNameVisitor : sphereScript99BaseVisitor<string>
    {
        public override string VisitCustomMemberAccess([NotNull] sphereScript99Parser.CustomMemberAccessContext context)
        {
            return context.memberName().GetText();
        }

        public override string VisitNativeMemberAccess([NotNull] sphereScript99Parser.NativeMemberAccessContext context)
        {
            return context.nativeFunctionName().GetText();
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
