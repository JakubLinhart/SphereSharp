namespace SphereSharp.ServUO.Sphere
{
    public class CSphereUIDBase
    {
        public int Serial { get; private set; }
        
        public CSphereUIDBase(int serial)
        {
            Serial = serial;
        }
    }
}