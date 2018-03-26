using Sprache;

namespace SphereSharp.Syntax
{
    internal static class EvalMacroParser
    {
        public static Parser<EvalMacroSyntax> InnerMacro =>
            from call in EvalParser.Eval
            select new EvalMacroSyntax(call);

        public static Parser<EvalMacroSyntax> Macro =>
            NonLiteralMacro.Or(LiteralMacro.Except(Parse.String("<?safe?>")));

        public static Parser<EvalMacroSyntax> NonLiteralMacro =>
            from _1 in CommonParsers.LeftMacroParenthesis
            from macro in InnerMacro
            from _2 in CommonParsers.RightMacroParenthesis
            select macro;

        public static Parser<EvalMacroSyntax> LiteralMacro =>
            from _1 in CommonParsers.LeftLiteralMacroParenthesis
            from macro in InnerMacro
            from _2 in CommonParsers.RightLiteralMacroParenthesis
            select macro;
    }
}
