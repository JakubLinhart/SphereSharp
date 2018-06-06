using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace SphereSharp.Sphere99.Sphere56Transpiler
{
    internal sealed class DialogPositionTranspiler : sphereScript99BaseVisitor<bool>
    {
        public DialogPositionTranspiler(SourceCodeBuilder builder, Sphere56TranspilerVisitor parentTranspiler)
        {
            this.builder = builder;
            this.parentTranspiler = parentTranspiler;
        }

        private readonly SourceCodeBuilder builder;
        private readonly Sphere56TranspilerVisitor parentTranspiler;

        public override bool VisitDialogSection([NotNull] sphereScript99Parser.DialogSectionContext context)
        {
            if (context.dialogPosition() != null)
            {
                parentTranspiler.Visit(context.dialogPosition());
                return true;
            }

            var statements = context.codeBlock()?.statement();
            if (statements != null)
            {
                foreach (var statement in statements)
                {
                    if (statement.call()?.firstMemberAccess() != null && VisitFirstMemberAccess(statement.call().firstMemberAccess()))
                        return true;
                    if (statement.assignment() != null && VisitAssignment(statement.assignment()))
                        return true;
                }
            }

            return false;
        }

        public override bool VisitFirstMemberAccess([NotNull] sphereScript99Parser.FirstMemberAccessContext context)
        {
            var name = new FirstMemberAccessNameVisitor().Visit(context);
            if (name.Equals("setlocation", StringComparison.OrdinalIgnoreCase))
            {
                var arguments = new FinalChainedMemberAccessArgumentsVisitor().Visit(context);
                if (arguments != null && arguments.Length > 0)
                {
                    parentTranspiler.AppendArguments(arguments);
                    builder.AppendLine();
                    return true;
                }
            }

            return base.VisitFirstMemberAccess(context);
        }

        public override bool VisitAssignment([NotNull] sphereScript99Parser.AssignmentContext context)
        {
            var name = new FinalChainedMemberAccessNameVisitor().Visit(context.firstMemberAccess());
            if (name.Equals("setlocation", StringComparison.OrdinalIgnoreCase))
            {
                var arguments = new FinalChainedMemberAccessArgumentsVisitor().Visit(context.argumentList());
                if (arguments != null && arguments.Length > 0)
                {
                    parentTranspiler.AppendArguments(arguments);
                    builder.AppendLine();
                    return true;
                }
            }

            return base.VisitAssignment(context);
        }
    }
}
