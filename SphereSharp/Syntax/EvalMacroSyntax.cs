using System.Collections.Generic;

namespace SphereSharp.Syntax
{
    public sealed class EvalMacroSyntax : SyntaxNode
    {
        public EvalSyntax Eval { get; }

        public EvalMacroSyntax(EvalSyntax eval)
        {
            Eval = eval;
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitEvalMacro(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield return Eval;
        }
    }
}