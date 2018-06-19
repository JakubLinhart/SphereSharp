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

        public override bool VisitSpeechTriggerList([NotNull] sphereScript99Parser.SpeechTriggerListContext context)
        {
            var triggerList = context.speechTrigger();
            if (triggerList != null)
                output.Append($"speechTriggers:{triggerList.Length};");
            else
                output.Append("speechTriggers:0;");

            return base.VisitSpeechTriggerList(context);
        }

        public override bool VisitCommentSection([NotNull] sphereScript99Parser.CommentSectionContext context)
        {
            var commentLines = context.commentLines();
            if (commentLines != null)
                output.Append($"commentLines:{commentLines.Length};");
            else
                output.Append("commentLines:0;");

            return base.VisitCommentSection(context);
        }

        public override bool VisitDialogButtonTriggerList([NotNull] sphereScript99Parser.DialogButtonTriggerListContext context)
        {
            var triggers = context.dialogButtonTrigger();
            if (triggers != null)
                output.Append($"buttonTriggers:{triggers.Length};");
            else
                output.Append("buttonTriggers:0;");

            return base.VisitDialogButtonTriggerList(context);
        }

        public override bool VisitNamesSection([NotNull] sphereScript99Parser.NamesSectionContext context)
        {
            var lines = context.freeTextLine();
            if (lines != null)
                output.Append($"free:{lines.Length};");
            else
                output.Append("free:0;");

            return base.VisitNamesSection(context);
        }

        public override bool VisitMenuTriggerList([NotNull] sphereScript99Parser.MenuTriggerListContext context)
        {
            var triggers = context.menuTrigger();
            if (triggers != null)
                output.Append($"menuTriggers:{triggers.Length};");
            else
                output.Append($"menuTriggers:0;");

            return base.VisitMenuTriggerList(context);
        }

        public override bool VisitScrollSection([NotNull] sphereScript99Parser.ScrollSectionContext context)
        {
            var freeTextLines = context.freeTextLine();
            if (freeTextLines != null)
                output.Append($"free:{freeTextLines.Length};");
            else
                output.Append("free:0;");

            return base.VisitScrollSection(context);
        }

        public override bool VisitPlevelSection([NotNull] sphereScript99Parser.PlevelSectionContext context)
        {
            var statements = context.codeBlock()?.statement();
            if (statements != null)
                output.Append($"stmts:{statements.Length};");
            else
                output.Append($"stmts:0;");

            return base.VisitPlevelSection(context);
        }

        public override bool VisitFunctionSection([NotNull] sphereScript99Parser.FunctionSectionContext context)
        {
            var statements = context.codeBlock()?.statement();
            if (statements != null)
                output.Append($"stmts:{statements.Length};");
            else
                output.Append($"stmts:0;");

            return base.VisitFunctionSection(context);
        }

        public override bool VisitSkillMenuSection([NotNull] sphereScript99Parser.SkillMenuSectionContext context)
        {
            var items = context.skillMenuItemList()?.skillMenuItem();
            if (items != null)
                output.Append($"menuItems:{items.Length};");
            else
                output.Append($"menuItems:0;");

            return base.VisitSkillMenuSection(context);
        }
    }
}
