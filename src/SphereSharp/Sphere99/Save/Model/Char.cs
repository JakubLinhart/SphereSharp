namespace SphereSharp.Sphere99.Save.Model
{
    public class Char : GameObject
    {
        public Char(string defName, ValuesHolder properties, ValuesHolder tags) : base(defName, properties, tags)
        {
        }

        public override bool IsPlayer => properties.IsDefined("Account");
        public override bool IsNpc => !IsPlayer;
        public override uint Amount => 1;
    }
}
