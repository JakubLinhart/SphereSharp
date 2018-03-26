using SphereSharp.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Runtime
{
    public class TestChar : IChar
    {
        private StringBuilder output = new StringBuilder();

        private StandardTagHolder tagHolder = new StandardTagHolder();

        public int Fame { get; set; }
        public int Karma { get; set; }

        public int MaxHits { get; set; }
        public int MaxStam { get; set; }
        public int MaxMana { get; set; }

        public int Parrying { get; set; }
        public int Tactics { get; set; }
        public int Wrestling { get; set; }
        public int SpiritSpeak { get; set; }

        public int Str { get; set; }
        public int Dex { get; set; }
        public int Int { get; set; }

        public int Color { get; set; }

        public int Npc { get; set; }

        public void RemoveTag(string key)
        {
            output.AppendLine($"RemoveTag {key}");

            tagHolder.RemoveTag(key);
        }

        public void Tag(string key, string value)
        {
            output.AppendLine($"Tag {key}, {value}");
            tagHolder.Tag(key, value);
        }

        public string Tag(string key)
        {
            var value = tagHolder.Tag(key);

            output.AppendLine($"Tag {key} => {value}");

            return value;
        }

        public string GetOutput() => output.ToString();
    }
}
