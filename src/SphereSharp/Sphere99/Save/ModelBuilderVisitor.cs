using Antlr4.Runtime.Misc;
using SphereSharp.Sphere99.Save.Model;

namespace SphereSharp.Sphere99.Save
{
    public class ModelBuilderVisitor : sphereScript99BaseVisitor<GameObjectRepository>
    {
        private readonly GameObjectRepository repository;

        public ModelBuilderVisitor(GameObjectRepository repository)
        {
            this.repository = repository;
        }

        public override GameObjectRepository VisitWorldCharSection([NotNull] sphereScript99Parser.WorldCharSectionContext context)
        {
            var defName = context.worldCharSectionHeader().sectionName().GetText();
            var builder = new GameObjectBuilderVisitor((props, tags) => new Model.Char(defName, props, tags));
            builder.Visit(context.propertyList());
            var gameChar = builder.Build();
            repository.Add(gameChar);

            return repository;
        }

        public override GameObjectRepository VisitWorldItemSection([NotNull] sphereScript99Parser.WorldItemSectionContext context)
        {
            var defName = context.worldItemSectionHeader().sectionName().GetText();
            var builder = new GameObjectBuilderVisitor((props, tags) => new Model.Item(defName, props, tags));
            builder.Visit(context.propertyList());
            var item = builder.Build();
            repository.Add(item);

            return repository;
        }
    }
}
