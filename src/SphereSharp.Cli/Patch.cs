using System.Collections.Generic;

namespace SphereSharp.Cli
{
    public sealed class Patch
    {
        public Patch()
        {
        }

        public Patch(IDictionary<string, string> patches)
        {
            this.Patches = patches;
        }

        public string Apply(string src)
        {
            foreach (var patch in Patches)
            {
                src = src.Replace(patch.Key, patch.Value);
            }

            return src;
        }

        public IDictionary<string, string> Patches { get; set; }
    }
}
