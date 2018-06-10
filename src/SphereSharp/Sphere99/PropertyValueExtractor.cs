using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Sphere99
{
    public sealed class PropertyValueExtractor : sphereScript99BaseVisitor<bool>
    {
        private bool found;
        private string foundValue;
        private string lookingForName;

        public override bool VisitPropertyAssignment([NotNull] sphereScript99Parser.PropertyAssignmentContext context)
        {
            var propertyName = context.propertyName().GetText();
            if (propertyName.Equals(lookingForName, StringComparison.OrdinalIgnoreCase))
            {
                foundValue = context.propertyValue()?.GetText();
                found = true;
                return true;
            }

            return base.VisitPropertyAssignment(context);
        }

        public bool TryExtract(string name, IParseTree tree, out string value)
        {
            found = false;
            lookingForName = name;

            Visit(tree);

            value = foundValue;
            return found;
        }
    }
}
