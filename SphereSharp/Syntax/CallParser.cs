using Sprache;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace SphereSharp.Syntax
{

    internal static class EventsArgumentParser
    {
        public static Parser<EventsArgumentSyntax> EventsArgument =>
            from sign in Parse.Char('+').Or(Parse.Char('-')).Optional()
            from name in SymbolParser.TextSegment
            select new EventsArgumentSyntax(name.Text, ToKind(sign));

        private static EventsOperationKind ToKind(IOption<char> sign)
        {
            if (sign.IsDefined)
            {
                switch (sign.Get())
                {
                    case '+':
                        return EventsOperationKind.Subscribe;
                    case '-':
                        return EventsOperationKind.Unsubscribe;
                    default:
                        throw new NotImplementedException($"Unknown {sign}");
                }
            }

            return EventsOperationKind.Subscribe;
        }
    }

    internal static class CallParser
    {
        public static Parser<CallSyntax> CustomCall =>
            from funcName in SymbolParser.Symbol
            from arguments in ArgumentListParser._ArgumentList
            select new CallSyntax(funcName, arguments);

        public static Parser<CallSyntax> Call =>
            InnerCall.Except(Parse.String("on="));

        public static Parser<CallSyntax> InnerCall =>
            from call in ArgvCall.Or(ChainedCall).Or(MemberCall)
            select call;

        public static Parser<StatementSyntax> CallStatement =
            from call in Call
            select call;

        public static Parser<CallSyntax> EventsCall =>
            from name in Parse.IgnoreCase("events").Text()
            from _1 in CommonParsers.OneLineWhiteSpace.AtLeastOnce()
            from argument in EventsArgumentParser.EventsArgument
            select new CallSyntax(new SymbolSyntax(name), new ArgumentListSyntax(new ArgumentSyntax[] { argument }.ToImmutableArray()));

        private static EventsOperationKind ToKind(IOption<char> sign)
        {
            if (sign.IsDefined)
            {
                switch (sign.Get())
                {
                    case '+':
                        return EventsOperationKind.Subscribe;
                    case '-':
                        return EventsOperationKind.Unsubscribe;
                    default:
                        throw new NotImplementedException($"Unknown {sign}");
                }
            }

            return EventsOperationKind.Subscribe;
        }


        public static Parser<CallSyntax> ArgvCall =>
            from name in Parse.IgnoreCase("argv").Text()
            from _2 in Parse.String("(")
            from expr in ExpressionParser.Expr
            from _3 in Parse.String(")")
            select new CallSyntax(new SymbolSyntax(name), 
                new ArgumentListSyntax(new ArgumentSyntax[] { new ExpressionArgumentSyntax(expr) }.ToImmutableArray()));

        public static Parser<CallSyntax> ParenthesesMemberCall =>
            from funcName in SymbolParser.Symbol
            from _ in CommonParsers.OneLineWhiteSpace.Many()
            from arguments in ArgumentListParser.ArgumentListWithParenthesis.Optional()
            select new CallSyntax(funcName,
                arguments.IsDefined ? arguments.Get() : ArgumentListSyntax.Empty);

        public static Parser<string> MembersWithoutParentheses =>
            from funcName in Parse.IgnoreCase("resizepic")
                .Or(Parse.IgnoreCase("dialog"))
                .Or(Parse.IgnoreCase("gumppic"))
                .Or(Parse.IgnoreCase("restest"))
                .Text()
            select funcName;

        public static Parser<ArgumentListSyntax> ArgumentListWithoutParenthesis =>
            from _ in CommonParsers.OneLineWhiteSpace.AtLeastOnce()
            from args in ArgumentListParser.ArgumentListWithoutParenthesis
            select args;


        public static Parser<CallSyntax> WithoutParenthesesMemberCall =>
            from funcName in MembersWithoutParentheses
            from arguments in ArgumentListParser.ArgumentListWithParenthesis.Or(ArgumentListWithoutParenthesis)
            select new CallSyntax(new SymbolSyntax(funcName), arguments);

        public static Parser<CallSyntax> MemberCall => EventsCall.Or(WithoutParenthesesMemberCall).Or(ParenthesesMemberCall);

        public static Parser<CallSyntax> ChainedCall =>
            from firstCall in MemberCall
            from nextCalls in ChainedCallInner.Many()
            select nextCalls.Count() > 0
                ? new CallSyntax(firstCall, nextCalls.Reverse().Aggregate((l, r) => new CallSyntax(r, l)))
                : firstCall;

        public static Parser<CallSyntax> ChainedCallForced =
            from firstCall in MemberCall
            from nextCalls in ChainedCallInner.AtLeastOnce()
            select nextCalls.Count() > 0
                ? new CallSyntax(firstCall, nextCalls.Reverse().Aggregate((l, r) => new CallSyntax(r, l)))
                : firstCall;

        public static Parser<CallSyntax> ChainedCallInner =>
            from _ in Parse.Char('.')
            from call in MemberCall
            select call;
    }
}
