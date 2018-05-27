using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace SphereSharp.Sphere99.Sphere56Transpiler
{
    public sealed class SafeTranspiler : sphereScript99BaseVisitor<bool>
    {
        private readonly SourceCodeBuilder builder;
        private readonly Sphere56TranspilerVisitor transpiler;
        private readonly FirstMemberAccessNameVisitor firstMemberAccessNameVisitor = new FirstMemberAccessNameVisitor();

        public SafeTranspiler(SourceCodeBuilder builder, Sphere56TranspilerVisitor transpiler)
        {
            this.builder = builder;
            this.transpiler = transpiler;
        }

        public override bool VisitNumericExpression([NotNull] sphereScript99Parser.NumericExpressionContext context)
        {
            var name = firstMemberAccessNameVisitor.Visit(context);
            if (name != null && !name.Equals("safe", StringComparison.OrdinalIgnoreCase))
                return false;

            return base.VisitNumericExpression(context);
        }

        public override bool VisitFirstMemberAccess([NotNull] sphereScript99Parser.FirstMemberAccessContext context)
        {
            var name = firstMemberAccessNameVisitor.Visit(context);
            if (!name.Equals("safe", StringComparison.OrdinalIgnoreCase))
                return false;

            return base.VisitFirstMemberAccess(context);
        }

        public override bool VisitChainedMemberAccess([NotNull] sphereScript99Parser.ChainedMemberAccessContext context)
        {
            transpiler.Visit(context.memberAccess());

            return true;
        }

        public override bool VisitFirstFreeArgumentOptionalWhiteSpace([NotNull] sphereScript99Parser.FirstFreeArgumentOptionalWhiteSpaceContext context)
        {
            if (context.evalExpression() != null)
            {
                transpiler.Visit(context.evalExpression());
                return true;
            }

            return false;
        }

        public override bool VisitEnclosedArgumentList([NotNull] sphereScript99Parser.EnclosedArgumentListContext context)
        {
            var arguments = context.argumentList().argument();
            if (arguments == null || arguments.Length != 1)
                throw new TranspilerException(context, "wrong number of arguments for safe method");

            transpiler.Visit(arguments[0]);

            return true;
        }
    }
}
