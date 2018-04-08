using SphereSharp.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace SphereSharp.Interpreter
{
    public class EvaluationContext
    {
        public object Src { get; set; }
        public IGump ArgO { get; set; }
        public Arguments Arguments { get; set; } = new Arguments();
        public EvaluationContext Parent { get; private set; }
        public object Default { get; set; }
        public Variables Variables { get; set; } = new Variables();

        public EvaluationContext CreateSubContext()
        {
            return new EvaluationContext()
            {
                Src = Src,
                ArgO = ArgO,
                Default = Default,
                Parent = this,
            };
        }
    }

    public class Variables
    {
        private Dictionary<string, string> variables = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public string Read(string name)
        {
            if (variables.TryGetValue(name, out string value))
                return value;

            throw new NotImplementedException($"Undefined variable {name}");
        }

        public void Set(string name, string value)
        {
            variables[name] = value;
        }
    }
}
