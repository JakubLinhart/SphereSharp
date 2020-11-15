using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Sphere99.Save.Model
{
    public static class GameObjectEnumerableExtensions
    {
        public static IEnumerable<GameObject> OfDefName(this IEnumerable<GameObject> collection, params string[] defNames)
            => collection.Where(obj => defNames.Contains(obj.DefName));
        public static IEnumerable<Item> OfDefName(this IEnumerable<Item> collection, params string[] defNames)
            => collection.Where(obj => defNames.Contains(obj.DefName));
        public static IEnumerable<GameObject> WithTag(this IEnumerable<GameObject> collection, params string[] tagNames)
            => collection.Where(obj => tagNames.Any(t => obj.HasTag(t)));
        public static IEnumerable<Char> WithTag(this IEnumerable<Char> collection, params string[] tagNames)
            => collection.Where(obj => tagNames.Any(t => obj.HasTag(t)));
    }
}
