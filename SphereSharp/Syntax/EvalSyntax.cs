using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Syntax
{
    public class EvalSyntax
    {
        public ExpressionSyntax Expression { get; }

        public EvalSyntax(ExpressionSyntax expression)
        {
            Expression = expression;
        }

        public static EvalSyntax Parse(string src) => EvalParser.Eval.Parse(src);
    }

    internal static class EvalParser
    {
        public static Parser<EvalSyntax> Eval =>
            from _1 in Parse.IgnoreCase("eval")
            from _2 in CommonParsers.OneLineWhiteSpace.AtLeastOnce()
            from expr in ExpressionParser.EqualityTerm
            select new EvalSyntax(expr);
    }
}
