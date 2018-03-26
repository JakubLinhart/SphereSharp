namespace SphereSharp.Model
{
    public sealed class SpellDef
    {
        public int Id { get; }
        public string DefName { get; }
        public string Name { get; }

        public SpellDef(int id, string defName, string name)
        {
            Id = id;
            DefName = defName;
            Name = name;
        }
    }
}
