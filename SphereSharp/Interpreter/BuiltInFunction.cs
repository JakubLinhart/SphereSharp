using System;

namespace SphereSharp.Interpreter
{
    public sealed class BuiltInFunction : Function
    {
        public Func<object, EvaluationContext, object> Implementation { get; }

        public BuiltInFunction(string name, Func<object, EvaluationContext, object> implementation)
            : base(name)
        {
            Implementation = implementation;
        }

        public override object Call(object targetObject, Evaluator evaluator, EvaluationContext context)
        {
            return Implementation(targetObject, context);
        }
    }
}
