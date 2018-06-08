using System;

namespace SphereSharp.Cli
{
    public sealed class PretranspilationReplacementScope
    {
        private readonly PretranspilationReplacements replacement;

        public string Name { get; }

        public PretranspilationReplacementScope(string name, PretranspilationReplacements replacement)
        {
            Name = name;
            this.replacement = replacement;
        }

        public string Apply(string src) => replacement.Apply(src);
    }
}
