using Sprache;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace SphereSharp.Syntax
{
    public class CodeBlockSyntax : SyntaxNode
    {
        public static CodeBlockSyntax Empty { get; }
            = new CodeBlockSyntax(ImmutableArray<StatementSyntax>.Empty);
        public ImmutableArray<StatementSyntax> Statements { get; }
        public bool IsEmpty => !Statements.Any();

        public CodeBlockSyntax(ImmutableArray<StatementSyntax> statements)
        {
            Statements = statements;
        }

        public static CodeBlockSyntax Parse(string src)
        {
            return CodeBlockParser.CodeBlock.Parse(src);
        }

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitCodeBlock(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            return Statements;
        }
    }
}
