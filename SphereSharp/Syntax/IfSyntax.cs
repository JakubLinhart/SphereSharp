using Sprache;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace SphereSharp.Syntax
{
    public sealed class IfSyntax : StatementSyntax
    {
        public ExpressionSyntax Condition { get; }
        public CodeBlockSyntax ThenBlock { get; }
        public ImmutableArray<ElseIfSyntax> ElseIfs { get; }
        public CodeBlockSyntax ElseBlock { get; }

        public IfSyntax(ExpressionSyntax condition, CodeBlockSyntax thenBlock, CodeBlockSyntax elseBlock, ImmutableArray<ElseIfSyntax> elseIfs)
        {
            Condition = condition;
            ThenBlock = thenBlock;
            ElseBlock = elseBlock;
            ElseIfs = elseIfs;
        }

        public static IfSyntax Parse(string src)
        {
            return IfSyntaxParser.If.Parse(src);
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitIf(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield return Condition;
            yield return ThenBlock;

            foreach (var elseIf in ElseIfs)
                yield return elseIf;

            yield return ElseBlock;
        }
    }

    public sealed class ElseIfSyntax : SyntaxNode
    {
        public ExpressionSyntax Condition { get; }
        public CodeBlockSyntax ThenBlock { get; }

        public ElseIfSyntax(ExpressionSyntax condition, CodeBlockSyntax thenBlock)
        {
            Condition = condition;
            ThenBlock = thenBlock;
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitElseIf(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield return Condition;
            yield return ThenBlock;
        }
    }
}
