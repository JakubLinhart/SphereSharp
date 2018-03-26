namespace SphereSharp.Syntax
{
    public sealed class MacroArgumentSyntax : ArgumentSyntax
    {
        public MacroSyntax Macro { get; }

        public MacroArgumentSyntax(MacroSyntax macro)
        {
            Macro = macro;
        }
    }
}
