using SphereSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace SphereSharp.Model
{
    public sealed class CodeModelBuilder
    {
        private List<DefNamesSectionSyntax> defNamesSections = new List<DefNamesSectionSyntax>();
        private Dictionary<string, DialogSectionSyntax> dialogSections = new Dictionary<string, DialogSectionSyntax>(StringComparer.OrdinalIgnoreCase);
        private Dictionary<string, DialogButtonsSectionSyntax> dialogButtonsSections = new Dictionary<string, DialogButtonsSectionSyntax>(StringComparer.OrdinalIgnoreCase);
        private Dictionary<string, DialogTextsSectionSyntax> dialogTextsSections = new Dictionary<string, DialogTextsSectionSyntax>(StringComparer.OrdinalIgnoreCase);
        private Dictionary<string, ItemDefSectionSyntax> itemDefSections = new Dictionary<string, ItemDefSectionSyntax>(StringComparer.OrdinalIgnoreCase);
        private Dictionary<string, CharDefSectionSyntax> charDefSections = new Dictionary<string, CharDefSectionSyntax>(StringComparer.OrdinalIgnoreCase);
        private Dictionary<string, FunctionSectionSyntax> functionSections = new Dictionary<string, FunctionSectionSyntax>(StringComparer.OrdinalIgnoreCase);
        private Dictionary<int, ProfessionSectionSyntax> professionSections = new Dictionary<int, ProfessionSectionSyntax>();
        private List<SpellSectionSyntax> spellSections = new List<SpellSectionSyntax>();
        private List<SkillSectionSyntax> skillSections = new List<SkillSectionSyntax>();

        public void Add(ItemDefSectionSyntax itemDef)
        {
            string defName = itemDef.GetPropertyValue("defname");
            itemDefSections[defName ?? itemDef.Name] = itemDef;
        }

        public void Add(CharDefSectionSyntax charDef)
        {
            string defName = charDef.GetPropertyValue("defname");
            charDefSections[defName ?? charDef.Name] = charDef;
        }

        public void Add(IEnumerable<SectionSyntax> sections)
        {
            foreach (var section in sections)
                Add(section);
        }

        public void Add(DefNamesSectionSyntax itemDef) => defNamesSections.Add(itemDef);
        public void Add(DialogSectionSyntax section) => dialogSections.Add(section.Name, section);
        public void Add(DialogButtonsSectionSyntax section) => dialogButtonsSections.Add(section.Name, section);
        public void Add(DialogTextsSectionSyntax section) => dialogTextsSections.Add(section.Name, section);
        public void Add(FunctionSectionSyntax section) => functionSections.Add(section.Name, section);
        public void Add(ProfessionSectionSyntax section) => professionSections.Add(section.Id, section);
        public void Add(SpellSectionSyntax section) => spellSections.Add(section);
        public void Add(SkillSectionSyntax section) => skillSections.Add(section);

        public void Add(SectionSyntax section)
        {
            switch (section)
            {
                case ItemDefSectionSyntax itemDef:
                    Add(itemDef);
                    break;
                case CharDefSectionSyntax charDef:
                    Add(charDef);
                    break;
                case DefNamesSectionSyntax defNames:
                    Add(defNames);
                    break;
                case DialogSectionSyntax dialog:
                    Add(dialog);
                    break;
                case DialogButtonsSectionSyntax dialogButtons:
                    Add(dialogButtons);
                    break;
                case DialogTextsSectionSyntax dialogTexts:
                    Add(dialogTexts);
                    break;
                case FunctionSectionSyntax function:
                    Add(function);
                    break;
                case ProfessionSectionSyntax profession:
                    Add(profession);
                    break;
                case SpellSectionSyntax spell:
                    Add(spell);
                    break;
                case SkillSectionSyntax skill:
                    Add(skill);
                    break;
            }
        }

        public CodeModel Build()
        {
            var itemDefs = BuildItemDefs();
            var charDefs = BuildCharDefs();
            var dialogs = BuildGumpDefs();
            var defNames = BuildDefNames();
            var functions = BuildFunctions();
            var professions = BuildProfessions();
            var spells = BuildSpells();
            var skills = BuildSkills();

            return new CodeModel(itemDefs, dialogs, defNames, functions, professions, spells, charDefs, skills);
        }

        private IEnumerable<ProfessionDef> BuildProfessions()
        {
            var professions = new List<ProfessionDef>();

            foreach (var section in professionSections.Values)
            {
                professions.Add(new ProfessionDef(section.Id, section.GetSinglePropertyValue("defname"),
                    section.GetSinglePropertyValue("name")));
            }

            return professions;
        }

        private IEnumerable<SpellDef> BuildSpells()
        {
            var spells = new List<SpellDef>();

            foreach (var section in spellSections)
            {
                spells.Add(new SpellDef(section.Id, section.GetSinglePropertyValue("defname"),
                    section.GetSinglePropertyValue("name")));
            }

            return spells;
        }

        private IEnumerable<SkillDef> BuildSkills()
        {
            var skills = new List<SkillDef>();

            foreach (var section in skillSections)
            {
                var skill = new SkillDef()
                {
                    Id = section.Id,
                    DefName = section.GetPropertyValue("defname"),
                    Triggers = BuildTriggers(section.Triggers),
                };
                skills.Add(skill);
            }

            return skills;

        }

        private IEnumerable<FunctionDef> BuildFunctions()
        {
            var functions = new List<FunctionDef>();

            foreach (var section in functionSections.Values)
            {
                functions.Add(new FunctionDef(section.Name, section.Body));
            }

            return functions;
        }

        private IEnumerable<NameDef> BuildDefNames()
        {
            var defNames = new List<NameDef>();

            foreach (var section in defNamesSections)
            {
                foreach (var defName in section.DefNames)
                {
                    defNames.Add(new NameDef(defName.LValue, defName.RValue));
                }
            }

            return defNames;
        }

        private IEnumerable<GumpDef> BuildGumpDefs()
        {
            var result = new List<GumpDef>();

            foreach (var dialogSection in dialogSections.Values)
            {
                ImmutableArray<string> texts;

                if (dialogTextsSections.TryGetValue(dialogSection.Name, out DialogTextsSectionSyntax textSection))
                {
                    texts = textSection.Texts;
                }
                else
                {
                    texts = ImmutableArray<string>.Empty;
                }

                ImmutableDictionary<string, TriggerDef> triggers;
                if (dialogButtonsSections.TryGetValue(dialogSection.Name, out DialogButtonsSectionSyntax buttonsSection))
                {
                    triggers = buttonsSection.Triggers.Select(x => new TriggerDef(x.Name, x.CodeBlock)).ToImmutableDictionary(x => x.Name);
                }
                else
                    triggers = ImmutableDictionary<string, TriggerDef>.Empty;

                var gumpDef = new GumpDef() { DefName = dialogSection.Name, InitCodeBlock = dialogSection.InitCodeBlock, Texts = texts, Triggers = triggers };
                result.Add(gumpDef);
            }

            return result;
        }

        private IEnumerable<CharDef> BuildCharDefs()
        {
            var sectionsToProcess = new HashSet<CharDefSectionSyntax>(charDefSections.Values);
            var charDefs = new Dictionary<string, CharDef>(StringComparer.OrdinalIgnoreCase);

            while (sectionsToProcess.Any())
            {
                var section = sectionsToProcess.First();
                BuildCharDef(section, charDefs, sectionsToProcess);
            }

            return charDefs.Values;
        }

        private ImmutableDictionary<string, TriggerDef> BuildTriggers(IEnumerable<TriggerSyntax> triggerSyntaxes)
        {
            var triggers = new Dictionary<string, TriggerDef>();

            foreach (var triggerSyntax in triggerSyntaxes)
            {
                triggers.Add(triggerSyntax.Name, new TriggerDef(triggerSyntax.Name, triggerSyntax.CodeBlock));
            }

            return triggers.ToImmutableDictionary(StringComparer.OrdinalIgnoreCase);
        }

        private void BuildCharDef(CharDefSectionSyntax section, Dictionary<string, CharDef> results, HashSet<CharDefSectionSyntax> sectionsToProcess)
        {
            CharDef charDef;

            var triggers = BuildTriggers(section.Triggers);

            if (ushort.TryParse(section.Name, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out ushort id))
            {
                charDef = new CharDef() { Id = id, DefName = section.GetPropertyValue("defname"), Triggers = triggers };
            }
            else
            {
                var baseId = section.GetPropertyValue("ID");
                if (!results.TryGetValue(baseId, out CharDef baseCharDef))
                {
                    if (!charDefSections.TryGetValue(baseId, out CharDefSectionSyntax baseSection))
                        throw new NotImplementedException($"Cannot find {baseId}");

                    BuildCharDef(baseSection, results, sectionsToProcess);

                    if (!results.TryGetValue(baseId, out baseCharDef))
                        throw new NotImplementedException();
                }

                charDef = new CharDef() { DefName = section.Name, Id = baseCharDef.Id, BaseCharDef = baseCharDef, Triggers = triggers };
            }

            charDef.Name = section.GetPropertyValue("name");
            charDef.Armor = section.GetPropertyNumberValue("armor");
            charDef.Attack = section.GetPropertyNumberValue("attack");
            charDef.MoveRate = section.GetPropertyNumberValue("moverate");

            results.Add(charDef.DefName, charDef);

            sectionsToProcess.Remove(section);
        }

        private IEnumerable<ItemDef> BuildItemDefs()
        {
            var sectionsToProcess = new HashSet<ItemDefSectionSyntax>(itemDefSections.Values);
            var itemDefs = new Dictionary<string, ItemDef>(StringComparer.OrdinalIgnoreCase);

            while (sectionsToProcess.Any())
            {
                var section = sectionsToProcess.First();
                BuildItemDef(section, itemDefs, sectionsToProcess);
            }

            return itemDefs.Values;
        }

        private void BuildItemDef(ItemDefSectionSyntax section, Dictionary<string, ItemDef> results, HashSet<ItemDefSectionSyntax> sectionsToProcess)
        {
            ItemDef itemDef;

            var triggers = BuildTriggers(section.Triggers);

            if (ushort.TryParse(section.Name, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out ushort id))
            {
                itemDef = new ItemDef() { Id = id, DefName = section.GetPropertyValue("defname"), Triggers = triggers };
            }
            else
            {
                var baseId = section.GetPropertyValue("ID");
                if (!results.TryGetValue(baseId, out ItemDef baseItemDef))
                {
                    if (!itemDefSections.TryGetValue(baseId, out ItemDefSectionSyntax baseSection))
                        throw new NotImplementedException($"Cannot find {baseId}");

                    BuildItemDef(baseSection, results, sectionsToProcess);

                    if (!results.TryGetValue(baseId, out baseItemDef))
                        throw new NotImplementedException();
                }

                itemDef = new ItemDef() { DefName = section.Name, Id = baseItemDef.Id, BaseItemDef = baseItemDef, Triggers = triggers };
            }

            results.Add(itemDef.DefName, itemDef);

            sectionsToProcess.Remove(section);
        }

        public void LoadDirectory(string path, string relativePath, TextWriter stdout)
        {
            foreach (var file in Directory.GetFiles(path, "*.scp"))
            {
                string fileName = Path.GetFileName(file);
                string relativeFile = relativePath != null ? Path.Combine(relativePath, fileName) : fileName;

                stdout.Write($"loading {relativeFile} ");
                var watch = Stopwatch.StartNew();
                var scpSrc = File.ReadAllText(file);
                var scpSyntax = FileSyntax.Parse(fileName, scpSrc);
                Add(scpSyntax.Sections);
                watch.Stop();

                stdout.WriteLine($"({watch.ElapsedMilliseconds}) ms");
            }

            foreach (var dir in Directory.GetDirectories(path))
            {
                string dirName = Path.GetFileName(dir);
                var relativeDir = relativePath != null ? Path.Combine(relativePath, dirName) : dirName;
                LoadDirectory(dir, relativeDir, stdout);
            }
        }
    }
}
