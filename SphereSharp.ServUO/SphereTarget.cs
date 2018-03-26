using Server;
using Server.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.ServUO
{
    public class SphereTarget : Target
    {
        public SphereTarget(bool allowGround, TargetFlags flags)
            : base(100, allowGround, flags)
        {
        }

        protected override void OnTarget(Mobile from, object targeted)
        {
            SphereSharpRuntime.Current.HandleTarget(from, targeted);
        }

        protected override void OnTargetFinish(Mobile from)
        {
        }
    }
}
