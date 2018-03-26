using Sprache;
using System.Linq;

namespace SphereSharp.Syntax
{
    internal static class PropertyParser
    {
        public static Parser<string> LValue =>
            from name in Parse.LetterOrDigit.Or(Parse.Char('_')).AtLeastOnce().Text()
            select name;

        public static Parser<string> RValue =>
            from text in Parse.AnyChar.Except(CommonParsers.Eol).AtLeastOnce().Text()
            select text;

        public static Parser<PropertySyntax> Property =>
            from _1 in CommonParsers.Ignored.Many()
            from lValue in LValue.Except(TriggerParser.TriggerPrefix).Once()
            from _2 in CommonParsers.OneLineWhiteSpace.Many()
            from _3 in Parse.Char('=').Once()
            from _4 in CommonParsers.OneLineWhiteSpace.Many()
            from rValue in RValue.Once()
            from _5 in CommonParsers.Eol
            select new PropertySyntax(lValue.Single(), rValue.Single());
    }


}
