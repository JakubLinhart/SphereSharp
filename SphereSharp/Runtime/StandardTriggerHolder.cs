using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SphereSharp.Interpreter;
using SphereSharp.Model;
using SphereSharp.Syntax;

namespace SphereSharp.Runtime
{
    public class StandardTriggerHolder : IHoldTriggers
    {
        private readonly Dictionary<string, TriggerDef> triggers;
        private readonly Func<CodeBlockSyntax, EvaluationContext, string> codeBlockRunner;

        public StandardTriggerHolder(IEnumerable<TriggerDef> triggers, Func<CodeBlockSyntax, EvaluationContext, string> codeBlockRunner)
        {
            this.triggers = triggers.ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);
            this.codeBlockRunner = codeBlockRunner;
        }

        public string Run(string triggerName, EvaluationContext context)
        {
            if (triggers.TryGetValue(triggerName, out TriggerDef trigger))
            {
                return codeBlockRunner(trigger.CodeBlock, context);
            }

            return string.Empty;
        }
    }
}
