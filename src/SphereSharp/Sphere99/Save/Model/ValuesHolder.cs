using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Sphere99.Save.Model
{
    public class ValuesHolder
    {
        private readonly Dictionary<string, List<string>> allValues = new Dictionary<string, List<string>>();

        internal void Add(string name, string value)
        {
            if (!allValues.TryGetValue(name, out var values))
            {
                values = new List<string>();
                allValues.Add(name, values);
            }
            values.Add(value);
        }

        private string GetSingleValue(string name) => allValues[name].Single();
        private bool TryGetSingleValue(string name, out string value)
        {
            if (allValues.TryGetValue(name, out var values))
            {
                value = values.Single();
                return true;
            }
            else
            {
                value = "";
                return false;
            }
        }

        public string GetSingle(string name) => allValues[name].Single();
        public bool TryGetSingle(string name, out string result)
        {
            if (allValues.TryGetValue(name, out var values))
            {
                result = values.Single();
                return true;
            }
            else
            {
                result = "";
                return false;
            }
        }

        public uint GetUInt(string name)
        {
            return uint.Parse(GetSingleValue(name));
        }

        public uint GetHexUInt(string name)
        {
            return uint.Parse(GetSingleValue(name), System.Globalization.NumberStyles.HexNumber);
        }

        public uint? GetUIntOrNull(string name)
        {
            if (TryGetSingleValue(name, out string value))
            {
                return uint.Parse(value);
            }
            else
                return null;
        }

        public uint? GetHexUIntOrNull(string name)
        {
            if (TryGetSingleValue(name, out string value))
            {
                return uint.Parse(value, System.Globalization.NumberStyles.HexNumber);
            }
            else
                return null;
        }

        public bool IsDefined(string name) => allValues.ContainsKey(name);
    }
}
