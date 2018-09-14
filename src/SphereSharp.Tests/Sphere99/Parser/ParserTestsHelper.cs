using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace SphereSharp.Tests.Sphere99.Parser
{
    public static class ParserTestsHelper
    {
        public static void CheckExpressionStructure(string src, string expectedResult)
        {
            Parse(src, parser =>
            {
                sphereScript99Parser.ConditionContext expression = parser.condition();
                EvalExpressionExtractor extractor = new EvalExpressionExtractor();
                extractor.Visit(expression);
                extractor.Result.Should().Be(expectedResult);
            });
        }

        public static void StatementShouldSucceed(string src)
        {
            CheckedParse(src, parser =>
            {
                sphereScript99Parser.StatementContext statement = parser.statement();
            });
        }

        public static void StatementShouldFail(string src)
        {
            try
            {
                Parse(src, parser =>
                {
                    sphereScript99Parser.StatementContext statement = parser.statement();
                });
            }
            catch (Exception)
            {
                return;
            }

            Assert.Fail($"Statement parse should fail: {src}");
        }

        public static void CheckedParse(string src, Action<sphereScript99Parser> parserAction)
        {
            try
            {
                Parse(src, parser =>
                {
                    if (parser.InputStream.Index + 1 < parser.InputStream.Size)
                    {
                        Assert.Fail($"Input stream not fully parsed index: {parser.InputStream.Index}, size: {parser.InputStream.Size}");
                    }

                    parserAction(parser);
                });
            }
            catch (Exception ex)
            {
                Assert.Fail($"Testing '{src.Substring(0, Math.Min(src.Length, 40))}'\n\nMessage: {ex.Message}\n\n{ex}");
            }
        }


        public static void Parse(string src, Action<sphereScript99Parser> parserAction)
        {
            AntlrInputStream inputStream = new AntlrInputStream(src);
            sphereScript99Lexer lexer = new sphereScript99Lexer(inputStream);
            CommonTokenStream tokenStream = new CommonTokenStream(lexer);
            sphereScript99Parser parser = new sphereScript99Parser(tokenStream);
            FailTestErrorListener errorListener = new FailTestErrorListener();
            parser.AddErrorListener(errorListener);

            parserAction(parser);
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
