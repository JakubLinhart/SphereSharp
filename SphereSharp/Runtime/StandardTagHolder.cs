using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Runtime
{
    public class StandardTagHolder : IHoldTags
    {
        private readonly Dictionary<string, string> tags = new Dictionary<string, string>();

        public void RemoveTag(string key)
        {
            tags.Remove(key);
        }

        public void Tag(string key, string value)
        {
            tags[key] = value;
        }

        public string Tag(string key)
        {
            if (tags.TryGetValue(key, out string value))
                return value;

            return string.Empty;
        }
    }
}
