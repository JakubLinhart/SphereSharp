using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;

namespace SphereSharp.Tests.Parser.Sphere99
{
    public class FirstLevelArgumentExtractor : sphereScript99BaseVisitor<bool>
    {
        private List<string> arguments = new List<string>();

        public string[] Arguments => arguments.ToArray();

        public override bool VisitExpressionArgument([NotNull] sphereScript99Parser.ExpressionArgumentContext context)
        {
            arguments.Add($"expr: {context.GetText()}");

            return true;
        }

        public override bool VisitQuotedLiteralArgument([NotNull] sphereScript99Parser.QuotedLiteralArgumentContext context)
        {
            arguments.Add($"quoted: {context.unquotedLiteralArgument()?.GetText() ?? string.Empty}");

            return true;
        }

        public override bool VisitUnquotedLiteralArgument([NotNull] sphereScript99Parser.UnquotedLiteralArgumentContext context)
        {
            arguments.Add($"unq: {context.GetText()}");

            return true;
        }

        public override bool VisitEvalExpression([NotNull] sphereScript99Parser.EvalExpressionContext context)
        {
            arguments.Add($"eval: {context.GetText()}");

            return true;
        }

        public override bool VisitEventArgument([NotNull] sphereScript99Parser.EventArgumentContext context)
        {
            arguments.Add($"event: {context.GetText()}");

            return true;
        }

        public override bool VisitTriggerArgument([NotNull] sphereScript99Parser.TriggerArgumentContext context)
        {
            arguments.Add($"trigger: {context.SYMBOL().GetText()}");

            return true;
        }

        public override bool VisitAssignmentArgument([NotNull] sphereScript99Parser.AssignmentArgumentContext context)
        {
            arguments.Add($"assignment: {context.GetText()}");

            return true;
        }
    }
}
