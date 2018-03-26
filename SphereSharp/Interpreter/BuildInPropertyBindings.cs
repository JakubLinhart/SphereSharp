using SphereSharp.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Interpreter
{
    public class BuildInPropertyBindings : IPropertyBinder
    {
        private Dictionary<string, Action<IChar, object>> charPropertySetters = new Dictionary<string, Action<IChar, object>>(StringComparer.OrdinalIgnoreCase)
        {
            { "fame", (c, v) => c.Fame = (int)v },
            { "karma", (c, v) => c.Karma = (int)v },
            { "maxhits", (c, v) => c.MaxHits= (int)v },
            { "maxstam", (c, v) => c.MaxStam= (int)v },
            { "maxmana", (c, v) => c.MaxMana= (int)v },

            { "str", (c, v) => c.Str= (int)v },
            { "dex", (c, v) => c.Dex= (int)v },
            { "int", (c, v) => c.Int= (int)v },

            { "Parrying", (c, v) => c.Parrying= (int)v },
            { "Tactics", (c, v) => c.Tactics= (int)v },
            { "Wrestling", (c, v) => c.Wrestling= (int)v },
            { "SpiritSpeak", (c, v) => c.SpiritSpeak= (int)v },
            { "color", (c, v) => c.Color = (int)v },
            { "npc", (c, v) => c.Npc = (int)v },
        };

        private Dictionary<string, Action<IItem, object>> itemPropertySetters = new Dictionary<string, Action<IItem, object>>(StringComparer.OrdinalIgnoreCase)
        {
            { "color", (c, v) => c.Color = (int)v },
        };

        private void SetProperty<T>(T targetObject, Dictionary<string, Action<T, object>> bindings, string name, object value)
        {
            if (bindings.TryGetValue(name, out Action<T, object> action))
            {
                action(targetObject, value);
            }
            else
                throw new NotImplementedException($"Cannot bind {name} to {targetObject.GetType().Name}");
        }

        public void SetProperty(object targetObject, string name, object value)
        {
            switch (targetObject)
            {
                case IChar ch:
                    SetProperty(ch, charPropertySetters, name, value);
                    break;
                case IItem item:
                    SetProperty(item, itemPropertySetters, name, value);
                    break;
                default:
                    throw new NotImplementedException($"Unknown target {targetObject.GetType().Name}");
            }
        }
    }
}
