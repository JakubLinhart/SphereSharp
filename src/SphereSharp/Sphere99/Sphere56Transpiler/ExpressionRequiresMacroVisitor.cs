using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace SphereSharp.Sphere99.Sphere56Transpiler
{
    public sealed class ExpressionRequiresMacroVisitor : sphereScript99BaseVisitor<bool>
    {
        public override bool VisitEvalExpression([NotNull] sphereScript99Parser.EvalExpressionContext context)
        {
            if (context.children.Count != 1 || context.signedEvalOperand() == null)
                return false;

            return Visit(context.signedEvalOperand());
        }

        public override bool VisitSignedEvalOperand([NotNull] sphereScript99Parser.SignedEvalOperandContext context)
        {
            if (context.ChildCount != 1 || context.evalOperand() == null)
                return false;

            return Visit(context.evalOperand());
        }

        public override bool VisitEvalOperand([NotNull] sphereScript99Parser.EvalOperandContext context)
        {
            if (context.ChildCount == 1 && context.firstMemberAccessExpression() == null)
            {
                return false;
            }

            return true;
        }
    }
}
