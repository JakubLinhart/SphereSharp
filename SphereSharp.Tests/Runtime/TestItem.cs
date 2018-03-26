using SphereSharp.Interpreter;
using SphereSharp.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Runtime
{
    public class TestItem : IItem
    {
        private IHoldTags tagHolder = new StandardTagHolder();

        public int Color { get; set; }

        public void RemoveTag(string key)
        {
            tagHolder.RemoveTag(key);
        }

        public string Run(string triggerName, EvaluationContext context)
        {
            throw new NotImplementedException();
        }

        public void Tag(string key, string value)
        {
            tagHolder.Tag(key, value);
        }

        public string Tag(string key)
        {
            return tagHolder.Tag(key);
        }
    }
}
