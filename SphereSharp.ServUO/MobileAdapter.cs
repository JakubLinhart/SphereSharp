using Server;
using Server.Gumps;
using SphereSharp.Interpreter;
using SphereSharp.Model;
using SphereSharp.Runtime;
using SphereSharp.ServUO.Sphere;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.ServUO
{
    public class MobileAdapter : IClient, IHoldTriggers
    {
        private readonly IHoldTags tagHolder = new StandardTagHolder();
        private readonly IHoldTriggers triggerHolder;
        private readonly Mobile mobile;
        public CClient SphereClient { get; }

        public MobileAdapter(Mobile mobile)
        {
            this.mobile = mobile;
            this.SphereClient = new CClient(mobile);
            triggerHolder = new StandardTriggerHolder(name => null, (syntax, context) => SphereSharpRuntime.Current.RunCodeBlock(syntax, context));
        }

        public void CloseDialog(string defName, int buttonId)
        {
            var gumpDef = SphereSharpRuntime.Current.CodeModel.GetGumpDef(defName);
            var gumpType = SphereSharpRuntime.Current.GetGumpType(defName);

            mobile.CloseGump(gumpType);
        }

        public void Dialog(string defName, Arguments arguments)
        {
            var gumpDef = SphereSharpRuntime.Current.CodeModel.GetGumpDef(defName);
            var gumpType = SphereSharpRuntime.Current.GetGumpType(defName);

            var gump = (Gump)Activator.CreateInstance(gumpType);
            SphereSharpRuntime.Current.InitializeDialog(gump, mobile, defName, arguments);

            this.mobile.SendGump(gump);
        }

        public void RemoveTag(string key)
        {
            tagHolder.RemoveTag(key);
        }

        public void SysMessage(string message)
        {
            this.mobile.SendMessage(message);
        }

        public void Tag(string key, string value)
        {
            tagHolder.Tag(key, value);
        }

        public string Tag(string key)
        {
            return tagHolder.Tag(key);
        }

        public void SubscribeEvents(EventsDef eventsDef)
        {
            this.triggerHolder.SubscribeEvents(eventsDef);
        }

        public void UnsubscribeEvents(EventsDef eventsDef)
        {
            this.triggerHolder.UnsubscribeEvents(eventsDef);
        }

        public string RunTrigger(string triggerName, EvaluationContext context)
        {
            return this.triggerHolder.RunTrigger(triggerName, context);
        }
    }
}
