namespace SphereSharp.Sphere99
{
    public interface IDefinitionsRepository
    {
        bool IsDefName(string name);
        bool IsGlobalVariable(string name);
    }
}