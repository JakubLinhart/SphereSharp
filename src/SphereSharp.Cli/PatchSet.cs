using System;
using System.Collections.Generic;

namespace SphereSharp.Cli
{
    public sealed class PatchSet
    {
        public IDictionary<string, string> Patches { get; set; }
        public string Scope { get; set; }

        public string Apply(string src)
        {
            foreach (var patch in Patches)
            {
                src = src.Replace(patch.Key, patch.Value);
            }

            return src;
        }
    }
}
