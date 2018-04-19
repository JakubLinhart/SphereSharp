using Sprache;
using System.Collections.Immutable;
using System.Linq;

namespace SphereSharp.Syntax
{
    internal static class CodeBlockParser
    {
        public static Parser<CodeBlockSyntax> CodeBlock =>
            from statements in Statement.Many()
            from _2 in CommonParsers.Eol.AtLeastOnce()
            select new CodeBlockSyntax(statements.ToImmutableArray());

        public static Parser<StatementSyntax> Statement =>
            from _1 in CommonParsers.Ignored.Many()
            from statement in Statements
            from _2 in CommonParsers.OneLineWhiteSpace.Many()
            select statement;

        public static Parser<StatementSyntax> Statements =>
            AssignmentParser.Assignment
                .Or(DoSwitchParser.DoSwitchStatement)
                .Or(MacroStatementParser.Statement)
                .Or(IfSyntaxParser.IfStatement)
                .Or(WhileStatementParser.While)
                .Or(ReturnParser.Return)
                .Or(CallParser.CallStatement);
    }
}
