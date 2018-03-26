using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.ServUO.Sphere
{
    public class CItem : CObjBasePtr
    {
        public CItem(int serial) : base(serial)
        {
        }

        public override void OnSpellEffect(SPELL_TYPE spell, CChar cChar, int iSkillLevel, CItem cItem)
        {
            throw new NotImplementedException();
        }

        public void SetTimeout(int iDelay)

        {
            Timer.DelayCall(TimeSpan.FromMilliseconds(iDelay), new TimerCallback(OnTimeout));
        }

        protected virtual void OnTimeout()
        {
        }
    }
}
