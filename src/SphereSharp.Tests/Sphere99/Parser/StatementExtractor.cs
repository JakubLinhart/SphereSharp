using Antlr4.Runtime.Misc;
using System.Text;

namespace SphereSharp.Tests.Sphere99.Parser
{
    public class StatementExtractor : sphereScript99BaseVisitor<bool>
    {
        private StringBuilder result = new StringBuilder();
        public string Result => result.ToString();

        public override bool VisitIfStatement([NotNull] sphereScript99Parser.IfStatementContext context)
        {
            if (context.codeBlock() != null)
                result.Append($"if({context.codeBlock().statement().Length});");
            else
                result.Append($"if(0);");

            base.VisitIfStatement(context);
            result.Append("endif;");

            return false;
        }

        public override bool VisitElseStatement([NotNull] sphereScript99Parser.ElseStatementContext context)
        {
            if (context.codeBlock() != null)
                result.Append($"else({context.codeBlock().statement().Length});");
            else
                result.Append($"else(0);");

            return false;
        }

        public override bool VisitElseIfStatement([NotNull] sphereScript99Parser.ElseIfStatementContext context)
        {
            if (context.codeBlock() != null)
                result.Append($"elseif({context.codeBlock().statement().Length});");
            else
                result.Append($"elseif(0);");

            return false;
        }

        public override bool VisitWhileStatement([NotNull] sphereScript99Parser.WhileStatementContext context)
        {
            if (context.codeBlock() != null)
                result.Append($"while({context.codeBlock().statement().Length});endwhile;");
            else
                result.Append($"while(0);endwhile;");

            return false;
        }

        public override bool VisitDoswitchStatement([NotNull] sphereScript99Parser.DoswitchStatementContext context)
        {
            result.Append($"doswitch({context.codeBlock().children.Count});enddo;");

            return false;
        }

        public override bool VisitDorandStatement([NotNull] sphereScript99Parser.DorandStatementContext context)
        {
            result.Append($"dorand({context.codeBlock().children.Count});enddo;");

            return false;
        }
    }
}
