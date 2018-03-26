namespace SphereSharp.Syntax
{
    public sealed class DefNameSyntax
    {
        public string LValue { get; }
        public string RValue { get; }

        public DefNameSyntax(string lValue, string rValue)
        {
            this.LValue = lValue;
            this.RValue = rValue;
        }
    }
}
