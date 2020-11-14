namespace SphereSharp.Sphere99.Save
{
    public class ObjectStats
    {
        public ObjectStats(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public long Amount { get; private set; }
        public int InstanceCount { get; private set; }

        public void AddInstance(int amount)
        {
            Amount += amount;
            InstanceCount++;
        }
    }
}
