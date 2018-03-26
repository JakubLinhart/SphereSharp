using System.Linq;
using Sprache;

namespace SphereSharp.Syntax
{
    internal static class AssignmentParser
    {
        public static Parser<AssignmentSyntax> Assignment =>
            from lValue in MemberAccessParser.MemberAccess.Except(Parse.String("on"))
            from _ in Parse.String("=")
            from rValue in ExpressionParser.Expr
            select new AssignmentSyntax(lValue, rValue);
    }
}
