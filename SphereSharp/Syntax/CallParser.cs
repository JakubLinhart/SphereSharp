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
            from obj in DottedCall.Many()
            from member in MemberCall
            select obj.Any() ? new CallSyntax(obj.Aggregate((l, r) => new CallSyntax(l, r)), member) : member;

        public static Parser<StatementSyntax> CallStatement =
            from call in Call
            select call;

        public static Parser<CallSyntax> MemberCall =>
            from funcName in SymbolParser.Symbol
            from _ in CommonParsers.OneLineWhiteSpace.Many()
            from arguments in ArgumentListParser.ArgumentList.Optional()
            select new CallSyntax(funcName, arguments.IsDefined ? arguments.Get() : ArgumentListSyntax.Empty);

        public static Parser<CallSyntax> DottedCall =>
            from funcName in SymbolParser.Symbol
            from arguments in ArgumentListParser.ArgumentListWithParenthesis.Optional()
            from _ in Parse.Char('.')
            select new CallSyntax(funcName, arguments.IsDefined 
                ? new ArgumentListSyntax(arguments.Get().ToImmutableArray())
                : ArgumentListSyntax.Empty);
    }
}
