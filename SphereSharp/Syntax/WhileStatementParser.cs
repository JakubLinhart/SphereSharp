using Sprache;
using System.Linq;

namespace SphereSharp.Syntax
{
    internal static class WhileStatementParser
    {
        public static Parser<WhileStatementSyntax> While =>
            from _0 in CommonParsers.OneLineWhiteSpace.Many()
            from _1 in CommonParsers.WhileKeyword
            from _2 in CommonParsers.OneLineWhiteSpace.Many()
            from condition in ExpressionParser.Expr
            from _3 in CommonParsers.Ignored
            from body in CodeBlockParser.CodeBlock
            from _4 in CommonParsers.Ignored
            from _5 in CommonParsers.EndWhileKeyword
            select new WhileStatementSyntax(condition, body);
    }
}
