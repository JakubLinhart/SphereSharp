namespace SphereSharp.Syntax
{
    public sealed class MacroSegmentSyntax : SegmentSyntax
    {
        public MacroSyntax Macro { get; }

        public MacroSegmentSyntax(MacroSyntax macro)
        {
            Macro = macro;
        }
    }
}
