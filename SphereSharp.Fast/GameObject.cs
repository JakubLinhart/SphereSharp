using System;
using System.Collections.Generic;
using System.Text;

namespace SphereSharp.Fast
{
    public sealed class GameObject
    {
        public uint Serial { get; set; }
        public string Defname { get; private set; }
        public int Amount { get; set; }
        public uint ContainerId { get; set; }
        public int TagGold { get; set; }
        public string More1 { get; set; }

        public GameObject(string defname)
        {
            Defname = defname;
        }
    }
}
