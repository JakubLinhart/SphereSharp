using Sprache;

namespace SphereSharp.Syntax
{
    internal static class MacroParser
    {
        public static Parser<MacroSyntax> InnerMacro =>
            from expr in ExpressionParser.EqualityTerm
            select new MacroSyntax(expr);

        public static Parser<MacroSyntax> Macro =>
            NonLiteralMacro.Or(LiteralMacro.Except(Parse.String("<?safe?>")));

        public static Parser<MacroSyntax> NonLiteralMacro =>
            from _1 in CommonParsers.LeftMacroParenthesis
            from macro in InnerMacro
            from _2 in CommonParsers.RightMacroParenthesis
            select macro;

        public static Parser<MacroSyntax> LiteralMacro =>
            from _1 in CommonParsers.LeftLiteralMacroParenthesis
            from macro in InnerMacro
            from _2 in CommonParsers.RightLiteralMacroParenthesis
            select macro;
    }
}
