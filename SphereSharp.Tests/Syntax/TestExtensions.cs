using FluentAssertions;
using FluentAssertions.Primitives;
using SphereSharp.Syntax;
using System;

namespace SphereSharp.Tests.Syntax
{
    public static class TestExtensions
    {
        public static object Argument(this ArgumentListSyntax list, int argumentIndex)
        {
            if (list.Arguments != null)
                return list.Arguments[argumentIndex];

            if (list._Arguments != null)
                return list._Arguments[argumentIndex];

            throw new InvalidOperationException();
        }

        public static void BeExpression(this ObjectAssertions assertions, string expressionSrc)
        {
            var actualExpression = assertions.Subject.Should().BeOfType<_ExpressionArgumentSyntax>().Which.Segments;
            var expectedExpression = _ExpressionSyntax.Parse(expressionSrc).Segments;

            actualExpression.Should().BeEquivalentTo(expectedExpression, options => options.IncludingAllRuntimeProperties());
        }
    }
}
