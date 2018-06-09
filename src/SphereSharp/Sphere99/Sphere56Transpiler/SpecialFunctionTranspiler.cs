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
                        transpiler.Visit(arguments[0]);

                        if (name.Equals("strcmpi", StringComparison.OrdinalIgnoreCase))
                        {
                            builder.StartSpecialFunctionArguments();
                            builder.Append(',');
                            transpiler.Visit(arguments[1]);
                            builder.EndSpecialFunctionArguments();
                        }
                    }
                    builder.Append(')');
                });

                return true;
            }

            return false;
        }
    }
}
