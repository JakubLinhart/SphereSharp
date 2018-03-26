using System.Collections.Generic;
using System.Text;

namespace SphereSharp.Interpreter
{
    public abstract class Function
    {
        public string Name { get; }

        protected Function(string name)
        {
            Name = name;
        }

        public abstract string Call(object targetObject, Evaluator evaluator, EvaluationContext context);
    }
}
