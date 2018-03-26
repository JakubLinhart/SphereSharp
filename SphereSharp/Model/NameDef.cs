namespace SphereSharp.Model
{
    public class NameDef
    {
        public string Key { get; }
        public string Value { get; }

        public NameDef(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}
