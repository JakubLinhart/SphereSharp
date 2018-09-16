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

        public override string VisitVariableFunctionName([NotNull] sphereScript99Parser.VariableFunctionNameContext context)
        {
            return context.GetText();
        }

        public override string VisitVariableAccess([NotNull] sphereScript99Parser.VariableAccessContext context)
        {
            return context.variableAssignment()?.variableFunctionName()?.GetText()
                ?? context.variableReadAccess()?.argumentedReadVariableAccess()?.variableFunctionName().GetText()
                ?? context.variableReadAccess()?.chainedReadVariableAccess()?.variableFunctionName().GetText()
                ?? context.variableRemoveAccess()?.argumentedVariableRemoveAccess()?.variableFunctionName().GetText()
                ?? context.variableRemoveAccess()?.chainedVariableRemoveAccess()?.variableFunctionName().GetText();
        }

        public override string VisitStrictNativeMemberAccess([NotNull] sphereScript99Parser.StrictNativeMemberAccessContext context)
        {
            return context.strictNativeFunctionName().GetText();
        }

        public override string VisitMacro([NotNull] sphereScript99Parser.MacroContext context)
        {
            if (context.escapedMacro()?.macroBody() != null)
                return Visit(context.escapedMacro().macroBody());
            if (context.nonEscapedMacro()?.macroBody() != null)
                return Visit(context.nonEscapedMacro().macroBody());

            return base.VisitMacro(context);
        }

        public override string VisitSignedEvalOperand([NotNull] sphereScript99Parser.SignedEvalOperandContext context)
        {
            if (context.evalOperand() != null)
                return Visit(context.evalOperand());

            return base.VisitSignedEvalOperand(context);
        }
    }
}
