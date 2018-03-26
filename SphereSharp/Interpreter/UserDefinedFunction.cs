using SphereSharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Interpreter
{
    internal class UserDefinedFunction : Function
    {
        private readonly FunctionDef functionDef;

        public UserDefinedFunction(string name, FunctionDef functionDef) : base(name)
        {
            this.functionDef = functionDef;
        }

        public override string Call(object targetObject, Evaluator evaluator, EvaluationContext context)
        {
            return evaluator.Evaluate(functionDef.Body, context);
        }
    }
}
