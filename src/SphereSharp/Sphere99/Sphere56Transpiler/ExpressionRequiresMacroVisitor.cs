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

        public override bool VisitArgumentExpression([NotNull] sphereScript99Parser.ArgumentExpressionContext context)
        {
            if (context.children.Count != 1 || context.signedArgumentOperand() == null)
                return true;

            return Visit(context.signedArgumentOperand());
        }

        public override bool VisitSignedEvalOperand([NotNull] sphereScript99Parser.SignedEvalOperandContext context)
        {
            if (context.ChildCount != 1 || context.evalOperand() == null)
                return false;

            return Visit(context.evalOperand());
        }

        public override bool VisitSignedArgumentOperand([NotNull] sphereScript99Parser.SignedArgumentOperandContext context)
        {
            if (context.ChildCount != 1 || context.argumentOperand() == null)
                return false;

            return Visit(context.argumentOperand());
        }

        public override bool VisitEvalOperand([NotNull] sphereScript99Parser.EvalOperandContext context)
        {
            if (context.ChildCount == 1 && context.firstMemberAccessExpression() == null)
            {
                return false;
            }

            return true;
        }

        public override bool VisitArgumentOperand([NotNull] sphereScript99Parser.ArgumentOperandContext context)
        {
            if (context.ChildCount == 1 && context.argumentSubExpression() == null)
                return false;

            return true;
        }
    }
}
