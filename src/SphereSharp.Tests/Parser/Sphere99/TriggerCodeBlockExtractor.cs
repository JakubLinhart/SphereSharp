using Antlr4.Runtime.Misc;
using System.Text;

namespace SphereSharp.Tests.Parser.Sphere99
{
    public class TriggerCodeBlockExtractor : sphereScript99BaseVisitor<bool>
    {
        StringBuilder output = new StringBuilder();
        public string Output => output.ToString();

        public override bool VisitTrigger([NotNull] sphereScript99Parser.TriggerContext context)
        {
            var result = base.VisitTrigger(context);

            if (context.triggerBody() != null)
                output.Append(context.triggerBody().codeBlock().statement().Length);
            else
                output.Append("0");

            output.Append(";");
            return result;
        }

        public override bool VisitTriggerName([NotNull] sphereScript99Parser.TriggerNameContext context)
        {
            output.Append(context.GetText());
            output.Append(":");

            return base.VisitTriggerName(context);
        }
    }
}
