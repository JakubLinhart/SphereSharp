using Sprache;
using System.Linq;

namespace SphereSharp.Syntax
{
    public static class MemberAccessParser
    {
        public static Parser<MemberAccessSyntax> MemberAccess =>
            from memberName in SymbolParser.Symbol
            from nextMembers in NextMemberName.Many()
            select nextMembers.Any()
                ? nextMembers.Aggregate(new MemberAccessSyntax(memberName, MemberAccessSyntax.GlobalObject), (l, r) => new MemberAccessSyntax(r, l))
                : new MemberAccessSyntax(memberName, MemberAccessSyntax.GlobalObject);

        public static Parser<string> NextMemberName =>
            from _ in Parse.Char('.')
            from memberName in CommonParsers.Symbol
            select memberName;
    }
}
