using Sprache;
using System.Linq;

namespace SphereSharp.Syntax
{
    internal static class DoSwitchParser
    {
        public static Parser<DoSwitchSyntax> DoSwitch =>
            from _2 in Parse.IgnoreCase("doswitch")
            from _3 in CommonParsers.OneLineWhiteSpace.AtLeastOnce()
            from expr in ExpressionParser.Expr
            from _4 in CommonParsers.Eol
            from statements in CodeBlockParser.CodeBlock
            from _5 in Parse.WhiteSpace.Many()
            from _6 in Parse.IgnoreCase("enddo")
            select new DoSwitchSyntax(expr, statements.Statements);

        public static Parser<StatementSyntax> DoSwitchStatement => DoSwitch;
    }
}
