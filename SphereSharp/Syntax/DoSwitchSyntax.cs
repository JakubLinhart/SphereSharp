using Sprache;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Syntax
{
    public class DoSwitchSyntax : StatementSyntax
    {
        public ExpressionSyntax Condition { get; }
        public ImmutableArray<StatementSyntax> Cases { get; }

        public DoSwitchSyntax(ExpressionSyntax condition, ImmutableArray<StatementSyntax> cases)
        {
            Condition = condition;
            Cases = cases;
        }

        public static DoSwitchSyntax Parse(string src)
            => DoSwitchParser.DoSwitch.Parse(src);
    }

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
