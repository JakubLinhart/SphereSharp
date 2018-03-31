using Sprache;
using System.Collections.Immutable;
using System.Linq;

namespace SphereSharp.Syntax
{
    internal static class CodeBlockParser
    {
        public static Parser<CodeBlockSyntax> CodeBlock =>
            from statements in Statement.Many()
            select new CodeBlockSyntax(statements.Where(x => x != null).ToImmutableArray());

        public static Parser<StatementSyntax> Statement =>
            from _1 in Parse.WhiteSpace.Optional()
            from statement in Statements.Optional()
            from _2 in CommonParsers.Eol.Optional()
            select statement.GetOrDefault();

        public static Parser<StatementSyntax> Statements =>
            AssignmentParser.Assignment
                .Or(EventsStatementParser.EventsStatement)
                .Or(DoSwitchParser.DoSwitchStatement)
                .Or(MacroStatementParser.Statement)
                .Or(IfSyntaxParser.IfStatement)
                .Or(ReturnParser.Return)
                .Or(CallParser.CallStatement);
    }
}
