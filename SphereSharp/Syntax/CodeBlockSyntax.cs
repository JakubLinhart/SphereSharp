using Sprache;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace SphereSharp.Syntax
{
    public class CodeBlockSyntax
    {
        public static CodeBlockSyntax Empty { get; }
            = new CodeBlockSyntax(ImmutableArray<StatementSyntax>.Empty);
        public ImmutableArray<StatementSyntax> Statements { get; }

        public CodeBlockSyntax(ImmutableArray<StatementSyntax> statements)
        {
            Statements = statements;
        }

        public static CodeBlockSyntax Parse(string src)
        {
            return CodeBlockParser.CodeBlock.Parse(src);
        }
    }
}
