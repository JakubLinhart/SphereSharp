namespace SphereSharp.Syntax
{
    public sealed class EvalMacroSyntax
    {
        public EvalSyntax Eval { get; }

        public EvalMacroSyntax(EvalSyntax eval)
        {
            Eval = eval;
        }
    }
}