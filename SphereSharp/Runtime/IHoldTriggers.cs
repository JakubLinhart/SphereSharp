using SphereSharp.Interpreter;
using SphereSharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Runtime
{
    public interface IHoldTriggers
    {
        void SubscribeEvents(EventsDef eventsDef);
        void UnsubscribeEvents(EventsDef eventsDef);
        string RunTrigger(string triggerName, EvaluationContext context);
    }
}
