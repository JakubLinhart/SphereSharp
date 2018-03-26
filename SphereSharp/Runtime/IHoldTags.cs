using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Runtime
{
    public interface IHoldTags
    {
        void Tag(string key, string value);
        string Tag(string key);
        void RemoveTag(string key);
    }
}
