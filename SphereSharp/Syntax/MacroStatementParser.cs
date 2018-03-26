using Sprache;
using System.Linq;

namespace SphereSharp.Syntax
{
    internal static class MacroStatementParser
    {
        public static Parser<StatementSyntax> Statement =>
            from macro in MacroParser.Macro
            select new MacroStatementSyntax(macro);
    }
}
