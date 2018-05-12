using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace SphereSharp.Sphere99
{
    public class Sphere56Transpiler : sphereScript99BaseVisitor<bool>
    {
        private SourceCodeBuilder builder = new SourceCodeBuilder();

        public string Output => builder.Output;

        public override bool VisitMemberName([NotNull] sphereScript99Parser.MemberNameContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitChainedMemberAccess([NotNull] sphereScript99Parser.ChainedMemberAccessContext context)
        {
            builder.Append(".");

            return base.VisitChainedMemberAccess(context);
        }

        public override bool VisitArgumentList([NotNull] sphereScript99Parser.ArgumentListContext context)
        {
            builder.Append(" ");

            var arguments = context.argument();
            for (int i = 0; i < arguments.Length; i++)
            {
                VisitArgument(arguments[i]);
                if (i < arguments.Length - 1)
                    builder.Append(",");
            }

            return true;
        }

        private string lastSharpSubstitution;

        public override bool VisitEvalOperand([NotNull] sphereScript99Parser.EvalOperandContext context)
        {
            if (context.GetText().Equals("#", StringComparison.OrdinalIgnoreCase))
            {
                builder.Append(lastSharpSubstitution);
                return true;
            }

            return base.VisitEvalOperand(context);
        }

        public override bool VisitEvalOperator([NotNull] sphereScript99Parser.EvalOperatorContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitConstantExpression([NotNull] sphereScript99Parser.ConstantExpressionContext context)
        {
            builder.Append(context.GetText());

            return true;
        }

        public override bool VisitFirstMemberAccess([NotNull] sphereScript99Parser.FirstMemberAccessContext context)
        {
            var name = context.customMemberAccess()?.memberName()?.GetText();
            if (!string.IsNullOrEmpty(name))
            {
                var arguments = context.customMemberAccess()?.enclosedArgumentList()?.argumentList()?.argument();

                if (name.Equals("arg", StringComparison.OrdinalIgnoreCase))
                {
                    if (arguments != null)
                    {
                        var localVariableAccess = $"LOCAL.{arguments[0].GetText()}";
                        builder.Append(localVariableAccess);
                        builder.Append('=');

                        try
                        {
                            lastSharpSubstitution = $"<{localVariableAccess}>";
                            return base.Visit(arguments[1]);
                        }
                        finally
                        {
                            lastSharpSubstitution = null;
                        }
                    }
                    else
                    {
                        throw new TranspilerException("No arguments for 'arg'");
                    }
                }
                else if (name.Equals("argcount", StringComparison.OrdinalIgnoreCase))
                {
                    builder.Append($"<argv>");
                    return true;
                }
                else if (name.Equals("argv", StringComparison.OrdinalIgnoreCase))
                {
                    if (arguments != null)
                    {
                        builder.Append($"<argv[");
                        base.Visit(arguments[0]);
                        builder.Append("]>");
                        return true;
                    }
                    else
                    {
                        throw new TranspilerException("No arguments for 'argv'");
                    }
                }
            }

            return base.VisitFirstMemberAccess(context);
        }
    }
}
