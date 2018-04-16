using Sprache;
using System;
using System.Collections.Generic;

namespace SphereSharp.Syntax
{
    internal static class SectionParser
    {
        public static Parser<IEnumerable<char>> SectionName =>
            NumericSectionName.Or(TextSectionName);

        public static Parser<IEnumerable<char>> TextSectionName =>
            Parse.AnyChar.Except(Parse.Char(']')).Many();

        public static Parser<IEnumerable<char>> NumericSectionName =>
            CommonParsers.IntegerHexNumber.Or(CommonParsers.IntegerDecadicNumber);

        public static Parser<(string type, string name)> SectionHeader =>
            from _1 in CommonParsers.Ignored.Many()
            from _2 in Parse.String("[")
            from sectionType in Parse.Letter.Many().Text()
            from _3 in CommonParsers.OneLineWhiteSpace.Many()
            from sectionName in SectionName.Text()
            from _6 in Parse.String("]")
            from _7 in CommonParsers.Ignored.Many()
            select (sectionType, sectionName);

        public static Parser<SectionSyntax> EofSection =>
            from _1 in CommonParsers.Ignored.Many()
            from _2 in Parse.LineTerminator
            select new EofSectionSyntax();

        public static Parser<SectionSyntax> Section =>
            from header in SectionHeader
            from section in ParseSection(header.type, header.name)
            select section;

        public static Parser<SectionSyntax> ParseSection(string sectionType, string sectionName)
        {
            switch (sectionType.ToLower())
            {
                case "function":
                    return FunctionSectionParser.ParseFunction(sectionType, sectionName);
                case "defnames":
                    return DefNamesSectionParser.ParseDefNames(sectionType, sectionName);
                case "itemdef":
                    return ItemDefSectionParser.ParseItemDef(sectionType, sectionName);
                case "chardef":
                    return CharDefSectionParser.ParseCharDef(sectionType, sectionName);
                case "dialog":
                    var sectionNameParts = sectionName.Split(' ');
                    sectionName = sectionNameParts[0];
                    if (sectionNameParts.Length == 1)
                    {
                        return DialogSectionParser.ParseDialog(sectionType, sectionName);
                    }
                    else if (sectionNameParts[1].Equals("button", StringComparison.OrdinalIgnoreCase))
                    {
                        return DialogButtonsSectionParser.ParseButtonSection(sectionType, sectionName, sectionNameParts[1]);
                    }
                    else if (sectionNameParts[1].Equals("text", StringComparison.OrdinalIgnoreCase))
                    {
                        return DialogTextsSectionParser.ParseTexts(sectionType, sectionName, sectionNameParts[1]);
                    }
                    else
                    {
                        throw new NotImplementedException($"subSectionName {sectionNameParts[1]}");
                    }
                case "profession":
                    return ProfessionSectionParser.ParseProfession(sectionType, sectionName);
                case "skill":
                    return SkillSectionParser.ParseSkillDef(sectionType, sectionName);
                case "spell":
                    return SpellSectionParser.ParseSpell(sectionType, sectionName);
                case "template":
                    return TemplateSectionParser.ParseTemplate(sectionType, sectionName);
                case "events":
                    return EventsSectionParser.ParseEvents(sectionType, sectionName);
                case "eof":
                    return EofSection;
                default:
                    throw new NotImplementedException($"sectionType {sectionType}");
            }
        }
    }
}
