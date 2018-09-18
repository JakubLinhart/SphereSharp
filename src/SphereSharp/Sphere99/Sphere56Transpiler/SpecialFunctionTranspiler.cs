using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace SphereSharp.Sphere99.Sphere56Transpiler
{
    internal sealed class SpecialFunctionTranspiler : sphereScript99BaseVisitor<bool>
    {
        private readonly FirstMemberAccessNameVisitor firstMemberAccessNameVisitor = new FirstMemberAccessNameVisitor();
        private readonly FirstMemberAccessArgumentsVisitor firstMemberAccessArgumentsVisitor = new FirstMemberAccessArgumentsVisitor();

        private readonly SourceCodeBuilder builder;
        private readonly Sphere56TranspilerVisitor transpiler;
        private readonly HashSet<string> specialFunctionNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "strlen",
            "strcmpi",
            "strcmp",
            "strmatch",
        };

        private bool IsSpecialFunction(string name) => specialFunctionNames.Contains(name);

        public SpecialFunctionTranspiler(SourceCodeBuilder builder, Sphere56TranspilerVisitor transpiler)
        {
            this.builder = builder;
            this.transpiler = transpiler;
        }

        public override bool VisitFirstMemberAccess([NotNull] sphereScript99Parser.FirstMemberAccessContext context)
        {
            var name = firstMemberAccessNameVisitor.Visit(context);
            IParseTree[] arguments = firstMemberAccessArgumentsVisitor.Visit(context);

            if (IsSpecialFunction(name))
            {
                builder.EnsureEvalCall("eval", () =>
                {
                    builder.Append(name);

                    builder.Append('(');
                    if (name.Equals("strmatch", StringComparison.OrdinalIgnoreCase))
                    {
                        builder.StartSpecialFunctionArguments();
                        transpiler.Visit(arguments[1]);
                        builder.Append(',');
                        transpiler.Visit(arguments[0]);
                        builder.EndSpecialFunctionArguments();
                    }
                    else
                    {
                        if (name.Equals("strcmpi", StringComparison.OrdinalIgnoreCase) ||
                            name.Equals("strcmp", StringComparison.OrdinalIgnoreCase))
                        {
                            builder.StartSpecialFunctionArguments();
                            QuoteIntrinsicArgument(arguments[0]);
                            builder.Append(',');
                            QuoteIntrinsicArgument(arguments[1]);
                            builder.EndSpecialFunctionArguments();
                        }
                        else
                            UnquoteIntrinsicArgument(arguments[0]);
                    }
                    builder.Append(')');
                });

                return true;
            }

            return false;
        }

        private void UnquoteIntrinsicArgument(IParseTree argument)
        {
            if (argument.GetChild(0) is sphereScript99Parser.QuotedLiteralArgumentContext quotedArgument)
            {
                var argumentTranspiler = new LiteralArgumentTranspiler(transpiler, builder, true);
                argumentTranspiler.Visit(quotedArgument);
            }
            else
            {
                transpiler.Visit(argument);
            }
        }

        private void QuoteIntrinsicArgument(IParseTree argument)
        {
            if (argument.GetChild(0) is sphereScript99Parser.QuotedLiteralArgumentContext) 
            {
                transpiler.Visit(argument);
            }
            else
            {
                builder.Append('"');
                transpiler.Visit(argument);
                builder.Append('"');
            }
        }
    }
}
