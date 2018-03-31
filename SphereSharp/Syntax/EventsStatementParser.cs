using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SphereSharp.Syntax
{
    internal static class EventsStatementParser
    {
        public static Parser<StatementSyntax> EventsStatement =>
            from ev in Events
            select ev;

        public static Parser<IEnumerable<char>> AssignmentOperator =>
            from _1 in CommonParsers.OneLineWhiteSpace.Many()
            from _2 in Parse.Char('=').Once()
            from _3 in CommonParsers.OneLineWhiteSpace.Many()
            select _1.Concat(_2).Concat(_3);

        public static Parser<EventsStatementSyntax> Events =>
            from _1 in Parse.IgnoreCase("events")
            from _2 in CommonParsers.OneLineWhiteSpace.Many().Or(AssignmentOperator)
            from sign in Parse.Char('+').Or(Parse.Char('-')).Optional()
            from name in SymbolParser.TextSegment
            select new EventsStatementSyntax(name.Text, ToKind(sign));

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
}
