using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Sphere99.Parser
{
    public static class ParserTestsHelper
    {
        public static void CheckExpressionStructure(string src, string expectedResult)
        {
            Parse(src, parser =>
            {
                var expression = parser.condition();
                var extractor = new EvalExpressionExtractor();
                extractor.Visit(expression);
                extractor.Result.Should().Be(expectedResult);
            });
        }

        public static void Parse(string src, Action<sphereScript99Parser> parserAction)
        {
            try
            {
                AntlrInputStream inputStream = new AntlrInputStream(src);
                var lexer = new sphereScript99Lexer(inputStream);
                var tokenStream = new CommonTokenStream(lexer);
                var parser = new sphereScript99Parser(tokenStream);
                var errorListener = new FailTestErrorListener();
                parser.AddErrorListener(errorListener);

                parserAction(parser);

                if (parser.InputStream.Index + 1 < parser.InputStream.Size)
                {
                    Assert.Fail($"Input stream not fully parsed index: {parser.InputStream.Index}, size: {parser.InputStream.Size}");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail($"Testing '{src.Substring(0, Math.Min(src.Length, 40))}'\n\nMessage: {ex.Message}\n\n{ex}");
            }
        }
    }

    public class EvalExpressionExtractor : sphereScript99BaseVisitor<bool>
    {
        private StringBuilder result = new StringBuilder();

        public string Result => result.ToString();

        public override bool VisitEvalBinaryOperator([NotNull] sphereScript99Parser.EvalBinaryOperatorContext context)
        {
            result.Append("operator:");
            result.Append(context.GetText());
            result.Append(';');

            return base.VisitEvalBinaryOperator(context);
        }

        public override bool VisitConstantExpression([NotNull] sphereScript99Parser.ConstantExpressionContext context)
        {
            result.Append("operand:");
            result.Append(context.GetText());
            result.Append(';');

            return base.VisitConstantExpression(context);
        }

        public override bool VisitFirstMemberAccessExpression([NotNull] sphereScript99Parser.FirstMemberAccessExpressionContext context)
        {
            result.Append("operand:");
            result.Append(context.GetText());
            result.Append(';');

            return base.VisitFirstMemberAccessExpression(context);
        }

        public override bool VisitIndexedMemberName([NotNull] sphereScript99Parser.IndexedMemberNameContext context)
        {
            result.Append("operand:");
            result.Append(context.GetText());
            result.Append(';');

            return true;
        }
    }

}
