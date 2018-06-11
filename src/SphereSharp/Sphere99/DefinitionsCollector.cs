using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace SphereSharp.Sphere99
{
    public sealed class DefinitionsCollector: sphereScript99BaseVisitor<bool>
    {
        private readonly DefinitionsRepository repository;

        public DefinitionsCollector(DefinitionsRepository repository)
        {
            this.repository = repository;
        }

        public override bool VisitDefNamesSection([NotNull] sphereScript99Parser.DefNamesSectionContext context)
        {
            var assignments = context.propertyList()?.propertyAssignment();

            if (assignments != null)
            {
                foreach (var assignment in assignments)
                {
                    var propertyName = assignment.propertyName().propertyNameText().GetText();
                    DefineProperty(propertyName);
                }
            }

            base.VisitDefNamesSection(context);
            return true;
        }

        public override bool VisitFunctionName([NotNull] sphereScript99Parser.FunctionNameContext context)
        {
            string functionName = context.GetText();
            repository.DefineFunctionName(functionName);

            return base.VisitFunctionName(context);
        }

        public override bool VisitCustomMemberAccess([NotNull] sphereScript99Parser.CustomMemberAccessContext context)
        {
            var name = context.memberName()?.GetText();
            if (!string.IsNullOrEmpty(name))
            {
                if (name.Equals("var", StringComparison.OrdinalIgnoreCase))
                {
                    var arguments = context.enclosedArgumentList()?.argumentList()?.argument();
                    if (arguments != null && arguments.Length > 1)
                    {
                        repository.DefineGlobalVariable(arguments[0].GetText().Trim());
                    }
                }
            }

            return base.VisitCustomMemberAccess(context);
        }

        private void DefineProperty(string propertyName) => repository.DefineDefName(propertyName);
    }
}
