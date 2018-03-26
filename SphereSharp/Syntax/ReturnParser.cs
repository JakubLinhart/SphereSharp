using Sprache;

namespace SphereSharp.Syntax
{
    internal static class ReturnParser
    {
        public static Parser<StatementSyntax> Return =>
            from keyword in Parse.IgnoreCase("return")
            from _1 in CommonParsers.OneLineWhiteSpace
            from arg in ArgumentListParser.Argument
            select new ReturnSyntax(arg);
    }
}
