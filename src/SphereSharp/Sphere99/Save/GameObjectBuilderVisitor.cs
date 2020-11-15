using Antlr4.Runtime.Misc;
using SphereSharp.Sphere99.Save.Model;
using System;

namespace SphereSharp.Sphere99.Save
{
    public class GameObjectBuilderVisitor : sphereScript99BaseVisitor<bool>
    {
        private readonly Func<ValuesHolder, ValuesHolder, GameObject> gameObjectFactory;
        private readonly ValuesHolder properties = new ValuesHolder();
        private readonly ValuesHolder tags = new ValuesHolder();

        public GameObjectBuilderVisitor(Func<ValuesHolder, ValuesHolder, GameObject> gameObjectFactory)
        {
            this.gameObjectFactory = gameObjectFactory;
        }

        public GameObject Build()
        {
            return gameObjectFactory(properties, tags);
        }

        public override bool VisitPropertyAssignment([NotNull] sphereScript99Parser.PropertyAssignmentContext context)
        {
            var propertyName = context.propertyName().GetText();
            var value = context.propertyValue()?.GetText().Trim('"');

            if (propertyName.StartsWith("tag.", StringComparison.OrdinalIgnoreCase))
            {
                var tagName = propertyName.Substring(4);
                tags.Add(tagName, value);
            }
            else
                properties.Add(propertyName, value);

            return true;
        }
    }
}
