using Sprache;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Syntax
{
    public class EvalSyntax
    {
        public ExpressionSyntax Expression { get; }
        public EvalKind Kind { get; }

        public EvalSyntax(ExpressionSyntax expression, EvalKind kind)
        {
            Expression = expression;
            Kind = kind;
        }

        public static EvalSyntax Parse(string src) => EvalParser.Eval.Parse(src);
    }
}
