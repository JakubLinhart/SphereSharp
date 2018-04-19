using System.Linq;
using Sprache;

namespace SphereSharp.Syntax
{
    internal static class AssignmentParser
    {
        public static Parser<AssignmentSyntax> Assignment =>
            from lValue in CallParser.Call.Except(Parse.String("on"))
            from _1 in CommonParsers.OneLineWhiteSpace.Many()
            from _2 in Parse.String("=")
            from _3 in CommonParsers.OneLineWhiteSpace.Many()
            from rValue in ArgumentListParser.Argument.Or(EmptyRValue)
            select new AssignmentSyntax(lValue, rValue);

        public static Parser<ArgumentSyntax> EmptyRValue =>
            from _1 in CommonParsers.OneLineWhiteSpace.Many()
            from _2 in CommonParsers.LineEnd
            select new EmptyArgumentSyntax();
    }
}
