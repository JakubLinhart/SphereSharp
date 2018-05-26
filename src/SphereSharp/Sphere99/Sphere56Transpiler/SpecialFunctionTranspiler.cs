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
            "strcmpi"
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
            bool inEval = false;

            if (context.evalCall()?.numericExpression() != null)
            {
                name = firstMemberAccessNameVisitor.Visit(context.evalCall().numericExpression());
                if (IsSpecialFunction(name))
                {
                    builder.Append("eval ");
                    arguments = firstMemberAccessArgumentsVisitor.Visit(context.evalCall().numericExpression());
                    inEval = true;
                }
                else
                    return false;
            }

            if (IsSpecialFunction(name))
            {
                if (!inEval)
                    builder.Append("<eval ");

                builder.Append(name);
                builder.Append('(');
                transpiler.Visit(arguments[0]);
                for (int i = 1; i < arguments.Length; i++)
                {
                    builder.Append(',');
                    transpiler.Visit(arguments[i]);
                }
                builder.Append(')');

                if (!inEval)
                    builder.Append('>');

                return true;
            }

            return false;
        }
    }
}
