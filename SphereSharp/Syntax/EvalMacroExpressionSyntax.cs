using System.Collections.Generic;

namespace SphereSharp.Syntax
{
    public class EvalMacroExpressionSyntax : ExpressionSyntax
    {
        public EvalMacroSyntax Macro { get; }
        public ExpressionSyntax Expression { get; }

        public EvalMacroExpressionSyntax(EvalMacroSyntax macro)
        {
            Macro = macro;
            Expression = macro.Eval.Expression;
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.AcceptEvalMacroExpression(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield return Macro;
            yield return Expression;
        }
    }
}
