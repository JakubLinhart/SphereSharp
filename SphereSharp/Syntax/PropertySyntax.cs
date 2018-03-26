namespace SphereSharp.Syntax
{
    public class PropertySyntax
    {
        public string LValue { get; }
        public string RValue { get; }

        public PropertySyntax(string lValue, string rValue)
        {
            this.LValue = lValue;
            this.RValue = rValue;
        }
    }
}
