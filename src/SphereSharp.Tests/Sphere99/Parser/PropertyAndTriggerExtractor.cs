using Antlr4.Runtime.Misc;
using System.Text;

namespace SphereSharp.Tests.Sphere99.Parser
{
    public class PropertyAndTriggerExtractor : sphereScript99BaseVisitor<bool>
    {
        private StringBuilder output = new StringBuilder();
        public string Output => output.ToString();

        public override bool VisitPropertyList([NotNull] sphereScript99Parser.PropertyListContext context)
        {
            var assignmentList = context.propertyAssignment();
            if (assignmentList != null)
                output.Append($"props:{assignmentList.Length};");
            else
                output.Append("props:0;");

            return base.VisitPropertyList(context);
        }

        public override bool VisitTriggerList([NotNull] sphereScript99Parser.TriggerListContext context)
        {
            var triggerList = context.trigger();
            if (triggerList != null)
                output.Append($"triggers:{triggerList.Length};");
            else
                output.Append("triggers:0;");

            return base.VisitTriggerList(context);
        }
    }
}
