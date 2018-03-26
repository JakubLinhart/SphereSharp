using SphereSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Runtime
{
    public interface IHoldTriggers
    {
        string Run(string triggerName, EvaluationContext context);
    }
}
