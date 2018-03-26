using System;

namespace SphereSharp.Interpreter
{
    public sealed class BuiltInFunction : Function
    {
        public Func<object, EvaluationContext, string> Implementation { get; }

        public BuiltInFunction(string name, Func<object, EvaluationContext, string> implementation)
            : base(name)
        {
            Implementation = implementation;
        }

        public override string Call(object targetObject, Evaluator evaluator, EvaluationContext context)
        {
            return Implementation(targetObject, context);
        }
    }
}
