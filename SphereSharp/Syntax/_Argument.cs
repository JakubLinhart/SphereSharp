using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Syntax
{
    public interface _IArgument {  }

    public abstract class _ArgumentSyntax : _IArgument
    {
        public static _ArgumentSyntax Parse(string src) => _ArgumentParser.Argument.Parse(src);
    }

    public sealed class _ExpressionSyntax
    {
        public _ExpressionSyntax(IEnumerable<_ExpressionSegmentSyntax> segments)
        {
            Segments = segments.ToArray();
        }

        public IEnumerable<_ExpressionSegmentSyntax> Segments { get; }

        public static _ExpressionSyntax Parse(string src) => _ArgumentParser.Expression.Parse(src);
    }

    public abstract class _ExpressionSegmentSyntax
    {
    }

    public sealed class _NumberExpressionSegmentSyntax : _ExpressionSegmentSyntax
    {
        public _NumberExpressionSegmentSyntax(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }

    public sealed class _OperatorExpressionSegmentSyntax : _ExpressionSegmentSyntax
    {
        public _OperatorExpressionSegmentSyntax(BinaryOperatorKind kind, _ExpressionSegmentSyntax operand2)
        {
            Kind = kind;
            Operand2 = operand2;
        }

        public BinaryOperatorKind Kind { get; }
        public _ExpressionSegmentSyntax Operand2 { get; }
    }

    public sealed class _SubExpressionSegmentSyntax : _ExpressionSegmentSyntax
    {
        public _SubExpressionSegmentSyntax(IEnumerable<_ExpressionSegmentSyntax> segments)
        {
            Segments = segments.ToArray();
        }

        public IEnumerable<_ExpressionSegmentSyntax> Segments { get; }
    }

    public sealed class _ExpressionArgumentSyntax : _ArgumentSyntax
    {
        public _ExpressionArgumentSyntax(IEnumerable<_ExpressionSegmentSyntax> segments)
        {
            Segments = segments.ToArray();
        }

        public IEnumerable<_ExpressionSegmentSyntax> Segments { get; }
    }

    public sealed class _TokenizedArgumentSyntax : _ArgumentSyntax
    {
    }

    internal static class _ArgumentParser
    {
        public static Parser<_ArgumentSyntax> Argument =>
            ExpressionArgument;

        public static Parser<_ArgumentSyntax> ExpressionArgument =>
            from segments in Segments
            select new _ExpressionArgumentSyntax(segments);

        public static Parser<_ExpressionSegmentSyntax> Number =>
            from number in Parse.Number
            select new _NumberExpressionSegmentSyntax(number);

        public static Parser<BinaryOperatorKind> BinaryOperator(string str, BinaryOperatorKind kind) =>
            from op in Parse.String(str)
            select kind;

        public static Parser<BinaryOperatorKind> AddOperator => BinaryOperator("+", BinaryOperatorKind.Add);
        public static Parser<BinaryOperatorKind> SubtractOperator => BinaryOperator("-", BinaryOperatorKind.Subtract);

        public static Parser<BinaryOperatorKind> Operator => AddOperator.Or(SubtractOperator);

        public static Parser<_ExpressionSegmentSyntax> Operation =>
            from op in Operator
            from operand2 in Number.Or(SubExpression)
            select new _OperatorExpressionSegmentSyntax(op, operand2);

        public static Parser<_ExpressionSegmentSyntax> SubExpression =>
            from _1 in Parse.Char('(')
            from segments in Segments
            from _2 in Parse.Char(')')
            select new _SubExpressionSegmentSyntax(segments);

        public static Parser<IEnumerable<_ExpressionSegmentSyntax>> Segments =>
            from firstSegment in Number.Or(SubExpression)
            from nextSegments in Operation.Many()
            select new _ExpressionSegmentSyntax[] { firstSegment }.Concat(nextSegments);


        public static Parser<_ExpressionSyntax> Expression =>
            from segments in Segments
            select new _ExpressionSyntax(segments);
    }

}
