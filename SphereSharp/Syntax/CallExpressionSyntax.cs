namespace SphereSharp.Syntax
{
    public class CallExpressionSyntax : ExpressionSyntax
    {
        public CallSyntax Call { get; }

        public CallExpressionSyntax(CallSyntax call)
        {
            this.Call = call;
        }
    }
}