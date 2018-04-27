using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Parser
{
    [TestClass]
    public class CustomFunctionArgumentListTests
    {
        [TestMethod]
        public void Can_parse_basic_expression_arguments()
        {
            ShouldSucceed("321", "expr: 321");
            ShouldSucceed("123,321", new[] { "expr: 123", "expr: 321" });
            ShouldSucceed("123,321,987", new[] { "expr: 123", "expr: 321", "expr: 987" });

            ShouldSucceed("1+1", "expr: 1+1");
            ShouldSucceed("1-1", "expr: 1-1");
            ShouldSucceed("1*1", "expr: 1*1");

            ShouldSucceed("1+1+1", "expr: 1+1+1");

            ShouldSucceed("(1)", "expr: (1)");
            ShouldSucceed("((1))", "expr: ((1))");
            ShouldSucceed("(1+1)", "expr: (1+1)");
            ShouldSucceed("(1+1)+1", "expr: (1+1)+1");
            ShouldSucceed("(1+1)+(1+1)", "expr: (1+1)+(1+1)");
            ShouldSucceed("(1+(1+1))+(1+1)", "expr: (1+(1+1))+(1+1)");
            ShouldSucceed("(1+(1+1))+(1+1),(2+(2+2))+(2+2)", new[] { "expr: (1+(1+1))+(1+1)", "expr: (2+(2+2))+(2+2)" });
            ShouldSucceed("(1+1),(2+2),(3+3)", new[] { "expr: (1+1)", "expr: (2+2)", "expr: (3+3)" });

            ShouldSucceed("<fun1(1)>", "expr: <fun1(1)>");
            ShouldSucceed("123,<fun1(1)>", new[] { "expr: 123", "expr: <fun1(1)>" });
            ShouldSucceed("<fun1(1)>,123", new[] { "expr: <fun1(1)>", "expr: 123" });
            ShouldSucceed("123,<fun1(1)>,123", new[] { "expr: 123", "expr: <fun1(1)>", "expr: 123" });
            ShouldSucceed("<tag(realm)>*(-1)", "expr: <tag(realm)>*(-1)");
        }

        [TestMethod]
        public void Can_parse_expression_arguments_with_negative_numbers()
        {
            Assert.Inconclusive();

            ShouldSucceed("-321", "expr: -321");
            ShouldSucceed("-(1)", "expr: -(1)");
            ShouldSucceed("1+-2", "expr: 1+-2");
            ShouldSucceed("1+-(2)", "expr: 1+-(2)");
            ShouldSucceed("1-(2)", "expr: 1-(2)");
        }

        [TestMethod]
        public void Can_parse_expression_arguments_with_multiple_macros()
        {
            ShouldSucceed("<fun1(<fun2(2)>)>", "expr: <fun1(<fun2(2)>)>");
            ShouldSucceed("<fun1(<fun2(2)>)>,123", new[] { "expr: <fun1(<fun2(2)>)>", "expr: 123" });
            ShouldSucceed("<fun1(1)>,<fun2(2)>", new[] { "expr: <fun1(1)>", "expr: <fun2(2)>" });
        }

        [TestMethod]
        public void Can_parse_unquoted_arguments()
        {
            ShouldSucceed("fun1", "unq: fun1");
            ShouldSucceed("fun1+1", "unq: fun1+1");
            ShouldSucceed("many words separated by space", "unq: many words separated by space");
            ShouldSucceed("many words separated by space,and other words as second argument",
                new[] { "unq: many words separated by space", "unq: and other words as second argument" });
            ShouldSucceed("fun1,fun2,fun3", new[] { "unq: fun1", "unq: fun2", "unq: fun3" });
        }

        [TestMethod]
        public void Can_parse_real_life_unquoted_arguments()
        {
            ShouldSucceed("<arg(pattern)>[-0123456789]", "unq: <arg(pattern)>[-0123456789]");
            ShouldSucceed("f_statistikapovolani(<arg(dny)>)", "unq: f_statistikapovolani(<arg(dny)>)");
        }

        [TestMethod]
        public void Can_parse_unquoted_arguments_with_macros()
        {
            ShouldSucceed("text1<macro(1)>", "unq: text1<macro(1)>");
            ShouldSucceed("text1<macro(1)>text2", "unq: text1<macro(1)>text2");
            ShouldSucceed("text1<macro(1)>text2<macro(2)>", "unq: text1<macro(1)>text2<macro(2)>");
            ShouldSucceed("text1<macro(1)>text2,text3<macro(2)>text4",
                new[] { "unq: text1<macro(1)>text2", "unq: text3<macro(2)>text4" });
        }

        private void ShouldSucceed(string src, params string[] expectedResults)
        {
            try
            {
                AntlrInputStream inputStream = new AntlrInputStream(src);
                var lexer = new sphereScript99Lexer(inputStream);
                var tokenStream = new CommonTokenStream(lexer);
                var parser = new sphereScript99Parser(tokenStream);

                var argumentList = parser.argumentList();

                var extractor = new FirstLevelArgumentExtractor();
                extractor.Visit(argumentList);
                extractor.Arguments.Should().BeEquivalentTo(expectedResults);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Testing '{src}'\n\nMessage: {ex.Message}\n\n{ex}");
            }
        }

        private class FirstLevelArgumentExtractor : sphereScript99BaseVisitor<bool>
        {
            private List<string> arguments = new List<string>();

            public string[] Arguments => arguments.ToArray();

            public override bool VisitExpressionArgument([NotNull] sphereScript99Parser.ExpressionArgumentContext context)
            {
                if (context.exception != null)
                    throw new InvalidOperationException($"Cannot visit invalid node: {context.GetText()}");

                arguments.Add($"expr: {context.GetText()}");

                return true;
            }

            public override bool VisitUnquotedLiteralArgument([NotNull] sphereScript99Parser.UnquotedLiteralArgumentContext context)
            {
                if (context.exception != null)
                    throw new InvalidOperationException($"Cannot visit invalid node: {context.GetText()}");

                arguments.Add($"unq: {context.GetText()}");

                return true;
            }
        }
    }
}
