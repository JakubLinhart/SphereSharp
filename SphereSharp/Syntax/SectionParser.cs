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
            Parse.Letter.Or(Parse.Char('_')).Many();

        public static Parser<IEnumerable<char>> NumericSectionName =>
            CommonParsers.IntegerHexNumber.Or(CommonParsers.IntegerDecadicNumber);

        public static Parser<(string type, string name, string subType)> SectionHeader =>
            from _1 in CommonParsers.Ignored.Many()
            from _2 in Parse.String("[")
            from sectionType in Parse.Letter.Many().Text()
            from _3 in CommonParsers.OneLineWhiteSpace.Many()
            from sectionName in SectionName.Text()
            from _4 in CommonParsers.OneLineWhiteSpace.Many()
            from subSection in SectionName.Text().Optional()
            from _5 in Parse.String("]")
            from _6 in CommonParsers.Ignored.Many()
            select (sectionType, sectionName, subSection.GetOrDefault());

        public static Parser<SectionSyntax> Section =>
            from header in SectionHeader
            from section in ParseSection(header.type, header.name, header.subType)
            select section;

        public static Parser<SectionSyntax> ParseSection(string sectionType, string sectionName, string sectionSubType)
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
                    if (string.IsNullOrEmpty(sectionSubType))
                    {
                        return DialogSectionParser.ParseDialog(sectionType, sectionName);
                    }
                    else if (sectionSubType.Equals("button", StringComparison.OrdinalIgnoreCase))
                    {
                        return DialogButtonsSectionParser.ParseButtonSection(sectionType, sectionName, sectionSubType);
                    }
                    else if (sectionSubType.Equals("text", StringComparison.OrdinalIgnoreCase))
                    {
                        return DialogTextsSectionParser.ParseTexts(sectionType, sectionName, sectionSubType);
                    }
                    else
                    {
                        throw new NotImplementedException($"subSectionName {sectionSubType}");
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
                default:
                    throw new NotImplementedException($"sectionType {sectionType}");
            }
        }
    }
}
