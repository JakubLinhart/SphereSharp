using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Sphere99.Save.Model
{
    public class GameObjectRepository
    {
        private Dictionary<uint, GameObject> objects = new Dictionary<uint, GameObject>();

        public IEnumerable<GameObject> Objects => objects.Values;
        public IEnumerable<Item> Items => objects.Values.OfType<Item>();
        public IEnumerable<GameObject> Chars => objects.Values.OfType<Char>();

        public GameObject GetTopmost(GameObject obj)
        {
            if (obj is Char)
                return obj;

            if (obj is Item item)
            {
                while (item.ContainerId.HasValue)
                    return GetTopmost(objects[item.ContainerId.Value]);
            }

            return obj;
        }

        internal void Add(GameObject gameObject)
        {
            objects.Add(gameObject.Serial, gameObject);
        }
    }
}
