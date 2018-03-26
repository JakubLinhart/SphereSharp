namespace SphereSharp.Syntax
{
    public sealed class LiteralArgumentSyntax : ArgumentSyntax
    {
        public LiteralSyntax Literal { get;  }

        public LiteralArgumentSyntax(LiteralSyntax literal)
        {
            Literal = literal;
        }
    }
}
