using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Sphere99.Sphere56Transpiler
{
    internal sealed class DialogFunctionArgumentsTranspiler : sphereScript99BaseVisitor<bool>
    {
        private readonly SourceCodeBuilder builder;
        private readonly Sphere56TranspilerVisitor parentTranspiler;

        public DialogFunctionArgumentsTranspiler(Sphere56TranspilerVisitor parentTranspiler, SourceCodeBuilder builder)
        {
            this.builder = builder;
            this.parentTranspiler = parentTranspiler;
        }

        public void AppendArguments(IEnumerable<IParseTree> arguments)
        {
            if (arguments == null)
                return;

            var argumentCount = arguments.Count();
            if (argumentCount == 0)
                return;

            var firstArgument = arguments.First();
            Visit(firstArgument);

            foreach (var argument in arguments.Skip(1))
            {
                builder.Append(' ');
                Visit(argument);
            }
        }

        public override bool VisitEvalExpression([NotNull] sphereScript99Parser.EvalExpressionContext context)
        {
            return parentTranspiler.Visit(context);
        }

        public override bool VisitUnquotedLiteralArgument([NotNull] sphereScript99Parser.UnquotedLiteralArgumentContext context)
        {
            return parentTranspiler.Visit(context);
        }

        public override bool VisitArgument([NotNull] sphereScript99Parser.ArgumentContext context)
        {
            if (context.quotedLiteralArgument() != null)
            {
                return new LiteralArgumentTranspiler(parentTranspiler, builder, true).Visit(context);
            }

            return parentTranspiler.Visit(context);
        }
    }
}
