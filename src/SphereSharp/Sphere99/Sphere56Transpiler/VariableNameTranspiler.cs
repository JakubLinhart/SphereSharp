using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace SphereSharp.Sphere99.Sphere56Transpiler
{
    public class VariableNameTranspiler : sphereScript99BaseVisitor<bool>
    {
        private readonly SourceCodeBuilder builder;
        private readonly Sphere56TranspilerVisitor parentTranspiler;

        public VariableNameTranspiler(SourceCodeBuilder builder, Sphere56TranspilerVisitor parentTranspiler)
        {
            this.builder = builder;
            this.parentTranspiler = parentTranspiler;
        }

        public override bool VisitGenericNativeMemberAccess([NotNull] sphereScript99Parser.GenericNativeMemberAccessContext context)
        {
            parentTranspiler.Visit(context);

            return true;
        }

        public override bool VisitStrictNativeFunctionName([NotNull] sphereScript99Parser.StrictNativeFunctionNameContext context)
        {
            parentTranspiler.Visit(context);

            return true;
        }

        public override bool VisitNativeFunctionName([NotNull] sphereScript99Parser.NativeFunctionNameContext context)
        {
            parentTranspiler.Visit(context);

            return true;
        }

        public override bool VisitIndexedMemberName([NotNull] sphereScript99Parser.IndexedMemberNameContext context)
        {
            Visit(context.indexedMemberNameCore());

            parentTranspiler.Visit(context.memberNameIndex());

            return true;
        }

        public override bool VisitQuotedLiteralArgument([NotNull] sphereScript99Parser.QuotedLiteralArgumentContext context)
        {
            var literalTranspiler = new LiteralArgumentTranspiler(parentTranspiler, builder, true);
            literalTranspiler.Visit(context);

            return true;
        }

        public override bool VisitEnclosedLiteralArgument([NotNull] sphereScript99Parser.EnclosedLiteralArgumentContext context)
        {
            var literalTranspiler = new LiteralArgumentTranspiler(parentTranspiler, builder, false);
            literalTranspiler.Visit(context);

            return true;
        }

        public override bool VisitMemberName([NotNull] sphereScript99Parser.MemberNameContext context)
        {
            var lastSegmentText = context.children.Last().GetText();
            var lastUnderscoreIndex = lastSegmentText.LastIndexOf('_');

            if (lastUnderscoreIndex >= 0 && lastUnderscoreIndex + 1 < lastSegmentText.Length)
            {
                var textAfterUnderscore = lastSegmentText.Substring(lastUnderscoreIndex + 1);
                if (int.TryParse(textAfterUnderscore, out int numberAfterUnderscore))
                {
                    parentTranspiler.AppendTerminalsVisitNodes(context.children.Take(context.children.Count - 1).ToArray());
                    builder.Append(lastSegmentText.Substring(0, lastUnderscoreIndex));
                    builder.Append('[');
                    builder.Append(textAfterUnderscore);
                    builder.Append(']');
                    return true;
                }
            }

            parentTranspiler.Visit(context);

            return true;
        }

        public override bool VisitMacro([NotNull] sphereScript99Parser.MacroContext context)
        {
            parentTranspiler.Visit(context);

            return true;
        }
    }
}
