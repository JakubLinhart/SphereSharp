using Server;
using Server.Gumps;
using Server.Network;
using Server.Targeting;
using SphereSharp.Interpreter;
using SphereSharp.Model;
using SphereSharp.Runtime;
using SphereSharp.ServUO.Sphere;
using SphereSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.ServUO
{
    public class SphereSharpRuntime
    {
        public static SphereSharpRuntime Current { get; set; }

        public CodeModel CodeModel { get; private set; }

        private static Dictionary<string, Type> gumpRegistry = new Dictionary<string, Type>();

        public void HandleSkillRequest(object caller, SkillRequestedArgs args)
        {
            var adapter = GetAdapter(args.Source);

            if (adapter != null)
            {
                adapter.SphereClient.Event_Skill_Use((SKILL_TYPE)args.SkillId);
            }
        }

        private void HandleCreated(IHoldTriggers triggerHolder)
        {
            var context = new EvaluationContext();
            context.Default = triggerHolder;
            triggerHolder.Run("create", context);
        }

        public void HandleItemCreated(ItemCreatedEventArgs e)
        {
            if (e.Item is IHoldTriggers triggerHolder)
                HandleCreated(triggerHolder);
        }

        public CharDef GetCharDef(string defName) => CodeModel.GetCharDef(defName);

        private readonly Dictionary<string, GumpAdapter> gumpAdapters = new Dictionary<string, GumpAdapter>();
        private readonly Dictionary<Mobile, MobileAdapter> mobileAdapters = new Dictionary<Mobile, MobileAdapter>();

        public void RegisterGump<TGump>(string defName) where TGump : Gump
        {
            gumpRegistry.Add(defName.ToLower(), typeof(TGump));
        }

        private MobileAdapter GetAdapter(Mobile mobile)
        {
            if (!mobileAdapters.TryGetValue(mobile, out MobileAdapter adapter))
            {
                adapter = new MobileAdapter(mobile);
                mobileAdapters.Add(mobile, adapter);
            }

            return adapter;
        }

        public string RunCodeBlock(CodeBlockSyntax codeBlock, EvaluationContext context)
        {
            string result = string.Empty;

            Protect(() =>
            {
                var evaluator = new Evaluator(CodeModel, new Binder(CodeModel));

                result = evaluator.Evaluate(codeBlock, context);
            });

            return result;
        }

        private void Protect(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Utility.PushColor(ConsoleColor.DarkRed);
                Console.WriteLine(ex.ToString());
                Utility.PopColor();
            }
        }

        public void RunDialogTrigger(string gumpDefName, Gump gump, NetState sender, RelayInfo info)
        {
            var gumpDef = CodeModel.GetGumpDef(gumpDefName);
            if (!gumpDef.Triggers.TryGetValue(info.ButtonID.ToString(), out TriggerDef triggerDef))
            {
                gumpDef.Triggers.TryGetValue("anybutton", out triggerDef);
            }

            if (triggerDef != null)
            {
                var context = new EvaluationContext();
                context.Arguments.ArgN = info.ButtonID;
                context.ArgO = GetGumpAdapter(gump, gumpDef);
                foreach (var entry in info.TextEntries)
                {
                    context.Arguments.AddTxt(entry.EntryID, entry.Text);
                }
                context.Src = GetAdapter(sender.Mobile);
                context.Default = context.Src;

                RunCodeBlock(triggerDef.CodeBlock, context);
            }
        }

        public Type GetGumpType(string defName)
        {
            return gumpRegistry[defName.ToLower()];
        }

        public void InitializeDialog(Gump gump, Mobile src, string gumpDefName, Arguments args)
        {
            var gumpDef = CodeModel.GetGumpDef(gumpDefName);

            var context = new EvaluationContext();
            context.ArgO = GetGumpAdapter(gump, gumpDef);
            context.Src = GetAdapter(src);
            context.Default = context.ArgO;
            context.Arguments = args;

            RunCodeBlock(gumpDef.InitCodeBlock, context);
        }

        private IGump GetGumpAdapter(Gump gump, GumpDef gumpDef)
        {
            if (!gumpAdapters.TryGetValue(gumpDef.DefName.ToLower(), out GumpAdapter gumpAdapter))
            {
                gumpAdapter = new GumpAdapter(gumpDef);
                gumpAdapters[gumpDef.DefName.ToLower()] = gumpAdapter;
            }

            gumpAdapter.Gump = gump;

            return gumpAdapter;
        }

        public SphereSharpRuntime()
        {
            Console.WriteLine("Initializing SphereSharp runtime");

            var watch = Stopwatch.StartNew();

            var codeModelBuilder = new CodeModelBuilder();
            codeModelBuilder.LoadDirectory(@"..\TestScripts", null, Console.Out);
            CodeModel = codeModelBuilder.Build();

            watch.Stop();
            Console.WriteLine($"SphereSharp runtime initialized in {watch.ElapsedMilliseconds} ms");
        }

        public void RunItemEvent(Mobile src, Item item, string itemDefName, string triggerName)
        {
            var itemDef = CodeModel.GetItemDef(itemDefName);
            var trigger = itemDef.Triggers[triggerName];

            var context = new EvaluationContext();
            context.Src = GetAdapter(src);
            context.Default = context.Src;

            RunCodeBlock(trigger.CodeBlock, context);
        }

        public void Target(bool allowGround, TargetFlags flags, Mobile mobile)
        {
            mobile.Target = new SphereTarget(allowGround, flags);
        }

        public void HandleCastSpellRequest(CastSpellRequestEventArgs e)
        {
            Protect(() =>
            {
                if (e.Spellbook != null)
                    throw new NotImplementedException();

                var adapter = GetAdapter(e.Mobile);
                adapter.SphereClient.Cmd_Skill_Magery((SPELL_TYPE)(e.SpellID + 1), adapter.SphereClient.m_pChar);
            });
        }

        internal void HandleTarget(Mobile from, object targeted)
        {
            Protect(() =>
            {
                var fromAdapter = GetAdapter(from);
                var targetedAdapter = GetAdapter((Mobile)targeted);

                switch (targeted)
                {
                    case LandTarget landTarget:
                        throw new NotImplementedException();
                        break;
                    case Mobile mobile:
                        var adapter = GetAdapter(mobile);
                        fromAdapter.SphereClient.OnTarg_Skill_Magery(adapter.SphereClient.m_pChar, new CPointMap());
                        break;
                }
            });
        }

        public CObjBasePtr FindObject(CSphereUID uid)
        {
            var mobile = World.Mobiles[uid.Serial];
            var adapter = GetAdapter(mobile);
            return adapter.SphereClient.m_pChar;
        }
    }
}
