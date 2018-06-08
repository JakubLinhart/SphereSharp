using System.Collections.Generic;

namespace SphereSharp.Cli
{
    public sealed class PretranspilationReplacements
    {
        public PretranspilationReplacements(IDictionary<string, string> replacements)
        {
            this.replacements = replacements;
        }

        public string Apply(string src)
        {
            foreach (var replacement in replacements)
            {
                src = src.Replace(replacement.Key, replacement.Value);
            }

            return src;
        }

        private readonly IDictionary<string, string> replacements;
    }
}
