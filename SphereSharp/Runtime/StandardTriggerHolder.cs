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
        private readonly Func<string, TriggerDef> triggerSource;
        private readonly Func<CodeBlockSyntax, EvaluationContext, string> codeBlockRunner;
        private readonly Dictionary<string, EventsDef> eventsSubscriptions = new Dictionary<string, EventsDef>();

        public StandardTriggerHolder(Func<string, TriggerDef> triggerSource, Func<CodeBlockSyntax, EvaluationContext, string> codeBlockRunner)
        {
            this.triggerSource = triggerSource;
            this.codeBlockRunner = codeBlockRunner;
        }

        public string RunTrigger(string triggerName, EvaluationContext context)
        {
            // TODO: semantics of events not clear, only the first found trigger runs,
            // own triggers have priority
            // reason for running just one trigger: triggers can returns something, if multiple
            // triggers run, then only last one can return something, this would be strange (further investigation needed)
            // no reason for prioritization, has to be investigated yet
            var triggerDef = triggerSource(triggerName);
            if (triggerDef != null)
            {
                return codeBlockRunner(triggerDef.CodeBlock, context);
            }

            foreach (var subscription in eventsSubscriptions.Values)
            {
                if (subscription.Triggers.TryGetValue(triggerName, out triggerDef))
                {
                    return codeBlockRunner(triggerDef.CodeBlock, context);
                }
            }

            return string.Empty;
        }

        public void SubscribeEvents(EventsDef eventsDef)
        {
            eventsSubscriptions.Add(eventsDef.Name, eventsDef);
        }

        public void UnsubscribeEvents(EventsDef eventsDef)
        {
            eventsSubscriptions.Remove(eventsDef.Name);
        }
    }
}
