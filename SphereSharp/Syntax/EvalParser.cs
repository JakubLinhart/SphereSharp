using Sprache;
using System;
using System.Linq;

namespace SphereSharp.Syntax
{
    internal static class EvalParser
    {
        public static EvalKind ToKind(string str)
        {
            switch (str)
            {
                case "eval":
                    return EvalKind.Decadic;
                case "hval":
                    return EvalKind.Hexadecimal;
                default:
                    throw new NotImplementedException($"EvalKind: {str}");
            }
        }

        public static Parser<EvalSyntax> Eval =>
            from evalKindStr in Parse.IgnoreCase("eval").Or(Parse.IgnoreCase("hval")).Text()
            from _2 in CommonParsers.OneLineWhiteSpace.AtLeastOnce()
            from expr in ExpressionParser.EqualityTerm
            select new EvalSyntax(expr, ToKind(evalKindStr));
    }
}
