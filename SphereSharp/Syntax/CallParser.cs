using Sprache;
using System.Collections.Immutable;
using System.Linq;

namespace SphereSharp.Syntax
{
    internal static class CallParser
    {
        public static Parser<CallSyntax> Call =>
            InnerCall.Except(Parse.String("on="));

        public static Parser<CallSyntax> InnerCall =>
            from call in ArgvCall.Or(ChainedCall).Or(MemberCall)
            select call;

        public static Parser<StatementSyntax> CallStatement =
            from call in Call
            select call;

        public static Parser<CallSyntax> ArgvCall =>
            from name in Parse.IgnoreCase("argv").Text()
            from _2 in Parse.String("(")
            from expr in ExpressionParser.Expr
            from _3 in Parse.String(")")
            select new CallSyntax(new SymbolSyntax(name), 
                new ArgumentListSyntax(new ArgumentSyntax[] { new ExpressionArgumentSyntax(expr) }.ToImmutableArray()));

        public static Parser<CallSyntax> MemberCall =>
            from funcName in SymbolParser.Symbol
            from _ in CommonParsers.OneLineWhiteSpace.Many()
            from arguments in ArgumentListParser.ArgumentList.Optional()
            select new CallSyntax(funcName, arguments.IsDefined ? arguments.Get() : ArgumentListSyntax.Empty);

        public static Parser<CallSyntax> ChainedCall =>
            from firstCall in MemberCall
            from nextCalls in ChainedCallInner.Many()
            select nextCalls.Count() > 0
                ? new CallSyntax(firstCall, nextCalls.Reverse().Aggregate((l, r) => new CallSyntax(r, l)))
                : firstCall;

        public static Parser<CallSyntax> ChainedCallInner =>
            from _ in Parse.Char('.')
            from call in MemberCall
            select call;
    }
}
