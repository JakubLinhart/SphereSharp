using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace SphereSharp.Model
{
    public sealed class CodeModel
    {
        private readonly ImmutableDictionary<string, ItemDef> itemDefs;
        private readonly ImmutableDictionary<string, CharDef> charDefs;
        private readonly ImmutableDictionary<string, GumpDef> gumpDefs;
        private readonly ImmutableDictionary<int, ProfessionDef> professionDefs;
        private readonly Dictionary<string, NameDef> defNames;
        private readonly ImmutableDictionary<string, FunctionDef> functions;

        public SpellDef GetSpellDef(int spellId) => spellDefsById[spellId];

        private readonly Dictionary<int, SpellDef> spellDefsById;
        private readonly Dictionary<string, SpellDef> spellDefsByDefName;

        public IEnumerable<GumpDef> GumpDefs => gumpDefs.Values;
        public IEnumerable<ItemDef> ItemDefs => itemDefs.Values;
        public IEnumerable<CharDef> CharDefs => charDefs.Values;
        public IEnumerable<ProfessionDef> ProfessionDefs => professionDefs.Values;

        public CodeModel(IEnumerable<ItemDef> itemDefs, IEnumerable<GumpDef> gumpDefs,
            IEnumerable<NameDef> defNames = null, IEnumerable<FunctionDef> functions = null,
            IEnumerable<ProfessionDef> professionDefs = null,
            IEnumerable<SpellDef> spellDefs = null,
            IEnumerable<CharDef> charDefs = null)
        {
            this.itemDefs = itemDefs?.ToImmutableDictionary(x => x.DefName.ToLower()) ?? ImmutableDictionary<string, ItemDef>.Empty;
            this.charDefs = charDefs?.ToImmutableDictionary(x => x.DefName.ToLower()) ?? ImmutableDictionary<string, CharDef>.Empty;
            this.gumpDefs = gumpDefs?.ToImmutableDictionary(x => x.DefName.ToLower()) ?? ImmutableDictionary<string, GumpDef>.Empty;
            this.defNames = defNames?.ToDictionary(x => x.Key.ToLower());
            this.functions = functions?.ToImmutableDictionary(x => x.Name.ToLower()) ?? ImmutableDictionary<string, FunctionDef>.Empty;
            this.professionDefs = professionDefs?.ToImmutableDictionary(x => x.Id) ?? ImmutableDictionary<int, ProfessionDef>.Empty;

            this.spellDefsById = spellDefs?.ToDictionary(x => x.Id) ?? new Dictionary<int, SpellDef>();
            this.spellDefsByDefName = spellDefs?.ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase) ?? new Dictionary<string, SpellDef>();
        }

        private TValue GetValue<TValue>(string key, IDictionary<string, TValue> dict, string exceptionMessageFormat)
        {
            if (dict.TryGetValue(key.ToLower(), out TValue value))
                return value;

            throw new InvalidOperationException(string.Format(exceptionMessageFormat, key));
        }

        public ItemDef GetItemDef(string name) => GetValue(name, itemDefs, "unknown item '{0}'");
        public CharDef GetCharDef(string name) => GetValue(name, charDefs, "unknown char '{0}'");
        public GumpDef GetGumpDef(string name) => GetValue(name, gumpDefs, "unknown gump '{0}'");
        public NameDef GetDefName(string name) => GetValue(name, defNames, "unknown defname '{0}'");

        public FunctionDef GetFunction(string name) => GetValue(name, functions, "unknown function '{0}'");
        public bool TryGetFunction(string name, out FunctionDef function) => functions.TryGetValue(name, out function);
    }
}
