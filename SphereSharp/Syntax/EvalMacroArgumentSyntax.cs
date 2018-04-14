using System.Collections.Generic;

namespace SphereSharp.Syntax
{
    public sealed class EvalMacroArgumentSyntax : ArgumentSyntax
    {
        public EvalMacroSyntax Macro { get; }
        public ExpressionSyntax Expression { get; }

        public EvalMacroArgumentSyntax(EvalMacroSyntax macro)
        {
            Macro = macro;
            Expression = macro.Eval.Expression;
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitEvalMacroArgument(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield return Macro;
        }
    }
}
