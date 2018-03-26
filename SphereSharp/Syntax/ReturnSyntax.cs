namespace SphereSharp.Syntax
{
    public sealed class ReturnSyntax : StatementSyntax
    {
        public ArgumentSyntax Argument { get; }

        public ReturnSyntax(ArgumentSyntax argument)
        {
            Argument = argument;
        }
    }
}
