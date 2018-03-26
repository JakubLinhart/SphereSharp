using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.ServUO.Sphere
{
    public class CObjBaseTemplate
    {
        CSphereUIDBase uidIndex;

        public CObjBaseTemplate(int serial)
        {
            uidIndex = new CSphereUID(serial);
        }

        public virtual CSphereUIDBase GetUID()
        {
            return uidIndex;
        }
    }
}
