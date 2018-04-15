using Sprache;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Syntax
{
    public class WhileStatementSyntax : StatementSyntax
    {
        public WhileStatementSyntax(ExpressionSyntax condition, CodeBlockSyntax body)
        {
            Condition = condition;
            Body = body;
        }

        public static WhileStatementSyntax Parse(string src) => WhileStatementParser.While.Parse(src);

        public ExpressionSyntax Condition { get; }
        public CodeBlockSyntax Body { get; }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitWhileStatement(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield return Condition;
            yield return Body;
        }
    }
}
