using SphereSharp.Interpreter;
using SphereSharp.Model;
using SphereSharp.Runtime;
using SphereSharp.Syntax;
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
        private StandardTriggerHolder triggerHolder;

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
        public int Action { get; set; }
        public void Skill(int skill)
        {
            output.AppendLine($"Start skill {skill}");
            Action = skill;
        }

        public void NewItem(string itemDefName)
        {
            output.AppendLine($"newitem {itemDefName}");

            lastNewItem = new TestItem();
        }

        private IItem lastNewItem;

        public IItem LastNew()
        {
            output.AppendLine("lastnew");

            return lastNewItem;
        }

        public TestChar(Func<string, TriggerDef> triggerSource, Func<CodeBlockSyntax, EvaluationContext, string> codeBlockRunner)
        {
            triggerHolder = new StandardTriggerHolder(triggerSource, codeBlockRunner);
        }

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

        public string RunTrigger(string triggerName, EvaluationContext context) =>
            triggerHolder.RunTrigger(triggerName, context);

        public void SubscribeEvents(EventsDef eventsDef) =>
            triggerHolder.SubscribeEvents(eventsDef);

        public void UnsubscribeEvents(EventsDef eventsDef) =>
            triggerHolder.UnsubscribeEvents(eventsDef);
    }
}
