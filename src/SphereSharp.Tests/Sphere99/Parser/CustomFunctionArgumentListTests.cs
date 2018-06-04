using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Sphere99.Parser
{
    [TestClass]
    public class CustomFunctionArgumentListTests : ParsingTestBase
    {
        [TestMethod]
        public void Can_parse_basic_expression_arguments()
        {
            CheckStructure("x(321)", "eval: 321");
            CheckStructure("x(0abc)", "eval: 0abc");
            CheckStructure("x(#0abc)", "eval: #0abc");
            CheckStructure("x(123,321)", new[] { "eval: 123", "eval: 321" });
            CheckStructure("x(123,321,987)", new[] { "eval: 123", "eval: 321", "eval: 987" });
            CheckStructure("x(123, 321, 987)", new[] { "eval: 123", "eval: 321", "eval: 987" });
            CheckStructure("x(1,)", "eval: 1", "empty");

            CheckStructure("x(1+1)", "eval: 1+1");
            CheckStructure("x(1-1)", "eval: 1-1");
            CheckStructure("x(1*1)", "eval: 1*1");
            CheckStructure("x(1/1)", "eval: 1/1");
            CheckStructure("x(1&1)", "eval: 1&1");
            CheckStructure("x(1|1)", "eval: 1|1");
            CheckStructure("x(1%1)", "eval: 1%1");

            CheckStructure("x(-1)", "eval: -1");
            CheckStructure("x(+1)", "eval: +1");
            CheckStructure("x(~1)", "eval: ~1");
            CheckStructure("x(!1)", "eval: !1");

            CheckStructure("x(1+1+1)", "eval: 1+1+1");

            CheckStructure("x((1))", "eval: (1)");
            CheckStructure("x(((1)))", "eval: ((1))");
            CheckStructure("x((1+1))", "eval: (1+1)");
            CheckStructure("x((1+1)+1)", "eval: (1+1)+1");
            CheckStructure("x((1+1)+(1+1))", "eval: (1+1)+(1+1)");
            CheckStructure("x((1+(1+1))+(1+1))", "eval: (1+(1+1))+(1+1)");
            CheckStructure("x((1+(1+1))+(1+1),(2+(2+2))+(2+2))", new[] { "eval: (1+(1+1))+(1+1)", "eval: (2+(2+2))+(2+2)" });
            CheckStructure("x((1+1),(2+2),(3+3))", new[] { "eval: (1+1)", "eval: (2+2)", "eval: (3+3)" });

            CheckStructure("x(<fun1(1)>)", "eval: <fun1(1)>");
            CheckStructure("x(123,<fun1(1)>)", new[] { "eval: 123", "eval: <fun1(1)>" });
            CheckStructure("x(<fun1(1)>,123)", new[] { "eval: <fun1(1)>", "eval: 123" });
            CheckStructure("x(123,<fun1(1)>,123)", new[] { "eval: 123", "eval: <fun1(1)>", "eval: 123" });
            CheckStructure("x(<tag(realm)>*(-1))", "eval: <tag(realm)>*(-1)");

            CheckStructure("x(fun1)", "eval: fun1");
            CheckStructure("x(fun1+1)", "eval: fun1+1");
            CheckStructure("x(fun1,fun2,fun3)", new[] { "eval: fun1", "eval: fun2", "eval: fun3" });
        }

        [TestMethod]
        public void Can_parse_random_expression()
        {
            CheckStructure("x({1 2})", "eval: {1 2}");
            CheckStructure("x({-2 -1})", "eval: {-2 -1}");
            CheckStructure("x({(1) (2)})", "eval: {(1) (2)}");
            CheckStructure("x({-(1) +(2)})", "eval: {-(1) +(2)}");
            CheckStructure("x({(1+1) (2+2)})", "eval: {(1+1) (2+2)}");
            CheckStructure("x({-<argv(0)> <argv(0)>})", "eval: {-<argv(0)> <argv(0)>}");
            CheckStructure("x({1 2}+{3 4})", "eval: {1 2}+{3 4}");
        }

        [TestMethod]
        public void Can_parse_signed_expression_arguments()
        {
            CheckStructure("x(-321)", "eval: -321");
            CheckStructure("x(-(1))", "eval: -(1)");
            CheckStructure("x(-( 1 ))", "eval: -( 1 )");
            CheckStructure("x(1+-2)", "eval: 1+-2");
            CheckStructure("x(1+-(2))", "eval: 1+-(2)");
            CheckStructure("x(1-(2))", "eval: 1-(2)");

            CheckStructure("x(+321)", "eval: +321");
            CheckStructure("x(+(1))", "eval: +(1)");
            CheckStructure("x(1++2)", "eval: 1++2");
            CheckStructure("x(1++(2))", "eval: 1++(2)");
            CheckStructure("x(1+(2))", "eval: 1+(2)");

            CheckStructure("x(1+++1,1---1)", new[] { "eval: 1+++1", "eval: 1---1" });
        }

        [TestMethod]
        public void Can_parse_expression_arguments_with_multiple_macros()
        {
            CheckStructure("x(<fun1(<fun2(2)>)>)", "eval: <fun1(<fun2(2)>)>");
            CheckStructure("x(<fun1(<fun2(2)>)>,123)", new[] { "eval: <fun1(<fun2(2)>)>", "eval: 123" });
            CheckStructure("x(<fun1(1)>,<fun2(2)>)", new[] { "eval: <fun1(1)>", "eval: <fun2(2)>" });
        }

        [TestMethod]
        public void Can_parse_unquoted_arguments()
        {
            CheckStructure("x(many words separated by space)", "unq: many words separated by space");
            CheckStructure("x(many words separated by space,and other words as second argument)",
                new[] { "unq: many words separated by space", "unq: and other words as second argument" });
            CheckStructure("x(?!)", "unq: ?!");
        }

        [TestMethod]
        public void Can_parse_quoted_arguments()
        {
            CheckStructure("x(\"can contain ''\")", "quoted: can contain ''");
            CheckStructure("x(\"can contain \\\")", "quoted: can contain \\");
            CheckStructure("x(\"some text\")", "quoted: some text");
            CheckStructure("x(1,\"some text\")", new[] { "eval: 1", "quoted: some text" });
            CheckStructure("x(\"some text\",1)", new[] { "quoted: some text", "eval: 1" });
            CheckStructure("x(1,\"some text\",2)", new[] { "eval: 1", "quoted: some text", "eval: 2" });
            CheckStructure("x(\"some text\",\"other text\")", new[] { "quoted: some text", "quoted: other text" });
            CheckStructure("x(\"<hours>:<mins>:<seconds>\")", "quoted: <hours>:<mins>:<seconds>");
            CheckStructure("x(\"some text with dot. and comma,\")", "quoted: some text with dot. and comma,");
            CheckStructure("x(\"\")", "quoted: ");
            CheckStructure("x(\"?!\")", "quoted: ?!");
            CheckStructure("x(\"Natocil <?arg(u)?> <?<findres(itemdef,<tag(id)>)>.name?> z uid=<?uid?>  (<?amount?> <?name?>) umisten <?p?> (<?region.name?>)\")", "quoted: Natocil <?arg(u)?> <?<findres(itemdef,<tag(id)>)>.name?> z uid=<?uid?>  (<?amount?> <?name?>) umisten <?p?> (<?region.name?>)");
            CheckStructure("x(\"<fun1(\"something\")>\")", "quoted: <fun1(\"something\")>");
        }

        [TestMethod]
        public void Can_parse_real_life_arguments()
        {
            CheckStructure("x(f_statistikapovolani(<arg(dny)>))", "eval: f_statistikapovolani(<arg(dny)>)");
            CheckStructure("x(\"veteran=vic jak 10. level\")", "quoted: veteran=vic jak 10. level");
            CheckStructure("x(<arg(pattern)>[-0123456789])", "eval: <arg(pattern)>[-0123456789]");
        }

        [TestMethod]
        public void Can_parse_eval_arguments_with_macros()
        {
            CheckStructure("x(text1<macro(1)>)", "eval: text1<macro(1)>");
            CheckStructure("x(text1<macro(1)>text2)", "eval: text1<macro(1)>text2");
            CheckStructure("x(text1<macro(1)>text2<macro(2)>)", "eval: text1<macro(1)>text2<macro(2)>");
            CheckStructure("x(text1<macro(1)>text2,text3<macro(2)>text4)",
                new[] { "eval: text1<macro(1)>text2", "eval: text3<macro(2)>text4" });
        }

        [TestMethod]
        public void Can_parse_assignment_argument()
        {
            CheckStructure("x(cont=1)", "assignment: cont=1");
        }

        private void CheckStructure(string src, params string[] expectedResults)
        {
            Parse(src, parser =>
            {
                var argumentList = parser.statement();

                var extractor = new FirstLevelArgumentExtractor();
                extractor.Visit(argumentList);
                extractor.Arguments.Should().BeEquivalentTo(expectedResults);
            });
        }
    }
}
