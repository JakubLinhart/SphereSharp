using Sprache;
using System.Collections.Immutable;

namespace SphereSharp.Syntax
{
    internal static class IfSyntaxParser
    {
        public static Parser<IfSyntax> If =>
            from _1 in CommonParsers.OneLineWhiteSpace.Many()
            from ifKeyword in CommonParsers.IfKeyword
            from _2 in CommonParsers.OneLineWhiteSpace.Many()
            from condition in ExpressionParser.Expr
            from _3 in CommonParsers.Ignored
            from thenBlock in CodeBlockParser.CodeBlock
            from elseIfs in ElseIf.Many()
            from elseBlock in Else.Optional()
            from _4 in CommonParsers.OneLineWhiteSpace.Many()
            from endifKeyword in CommonParsers.EndifKeyword
            select new IfSyntax(condition, thenBlock,
                elseBlock.IsDefined ? elseBlock.Get() : CodeBlockSyntax.Empty,
                elseIfs.ToImmutableArray());

        public static Parser<CodeBlockSyntax> Else =>
            from _1 in CommonParsers.OneLineWhiteSpace.Many()
            from elseKeyword in CommonParsers.ElseKeyword
            from _ in CommonParsers.Ignored
            from elseBlock in CodeBlockParser.CodeBlock
            select elseBlock;

        public static Parser<ElseIfSyntax> ElseIf =>
            from _1 in CommonParsers.OneLineWhiteSpace.Many()
            from elseIfKeyword in CommonParsers.ElseIfKeyword
            from _2 in CommonParsers.OneLineWhiteSpace.Many()
            from condition in ExpressionParser.Expr
            from _3 in CommonParsers.Ignored
            from thenBlock in CodeBlockParser.CodeBlock.Optional()
            select new ElseIfSyntax(condition, thenBlock.IsDefined ? thenBlock.GetOrDefault() : CodeBlockSyntax.Empty);

        internal static Parser<StatementSyntax> IfStatement =>
            from ifStatement in If
            select ifStatement;
    }
}
