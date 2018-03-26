using SphereSharp.Interpreter;
using SphereSharp.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Runtime
{
    public class TestClient : RuntimeTestObject, IClient
    {
        private readonly IHoldTags tagHolder = new StandardTagHolder();

        public void CloseDialog(string defName, int buttonId)
        {
            WriteLine($"closedialog {defName}, {buttonId}");
        }

        public void Dialog(string defName, Arguments args)
        {
            if (args.Count() > 0)
            {
                string argsText = string.Join(", ", args.ToArray());
                WriteLine($"dialog {defName}, {argsText}");
            }
            else
                WriteLine($"dialog {defName}");
        }

        public void SysMessage(string message)
        {
            WriteLine($"sysmessage {message}");
        }

        public void Tag(string key, string value)
        {
            WriteLine($"tag {key}, {value}");
            tagHolder.Tag(key, value);
        }

        public string Tag(string key)
        {
            WriteLine($"tag {key}");

            return tagHolder.Tag(key);
        }

        public void RemoveTag(string key)
        {
            WriteLine($"removetag {key}");

            tagHolder.RemoveTag(key);
        }
    }
}
