namespace SphereSharp.Sphere99.Save.Model
{
    public class Item : GameObject
    {
        public Item(string defName, ValuesHolder properties, ValuesHolder tags) : base(defName, properties, tags)
        {
        }

        public uint? ContainerId => properties.GetHexUIntOrNull("Cont");
        public uint More1 => properties.GetHexUIntOrNull("MORE1") ?? 0;
        public override bool IsPlayer => false;
        public override bool IsNpc => false;
        public override uint Amount => properties.GetUIntOrNull("Amount") ?? 1;
    }
}
