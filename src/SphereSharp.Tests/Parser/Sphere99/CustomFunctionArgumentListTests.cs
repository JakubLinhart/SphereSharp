using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Parser.Sphere99
{
    [TestClass]
    public class CustomFunctionArgumentListTests : ParsingTestBase
    {
        [TestMethod]
        public void Can_parse_basic_expression_arguments()
        {
            CheckStructure("(321)", "eval: 321");
            CheckStructure("(123,321)", new[] { "eval: 123", "eval: 321" });
            CheckStructure("(123,321,987)", new[] { "eval: 123", "eval: 321", "eval: 987" });

            CheckStructure("(1+1)", "eval: 1+1");
            CheckStructure("(1-1)", "eval: 1-1");
            CheckStructure("(1*1)", "eval: 1*1");
            CheckStructure("(1/1)", "eval: 1/1");
            CheckStructure("(1&1)", "eval: 1&1");
            CheckStructure("(1|1)", "eval: 1|1");
            CheckStructure("(1%1)", "eval: 1%1");

            CheckStructure("(-1)", "eval: -1");
            CheckStructure("(+1)", "eval: +1");
            CheckStructure("(~1)", "eval: ~1");
            CheckStructure("(!1)", "eval: !1");

            CheckStructure("(1+1+1)", "eval: 1+1+1");

            CheckStructure("((1))", "eval: (1)");
            CheckStructure("(((1)))", "eval: ((1))");
            CheckStructure("((1+1))", "eval: (1+1)");
            CheckStructure("((1+1)+1)", "eval: (1+1)+1");
            CheckStructure("((1+1)+(1+1))", "eval: (1+1)+(1+1)");
            CheckStructure("((1+(1+1))+(1+1))", "eval: (1+(1+1))+(1+1)");
            CheckStructure("((1+(1+1))+(1+1),(2+(2+2))+(2+2))", new[] { "eval: (1+(1+1))+(1+1)", "eval: (2+(2+2))+(2+2)" });
            CheckStructure("((1+1),(2+2),(3+3))", new[] { "eval: (1+1)", "eval: (2+2)", "eval: (3+3)" });

            CheckStructure("(<fun1(1)>)", "eval: <fun1(1)>");
            CheckStructure("(123,<fun1(1)>)", new[] { "eval: 123", "eval: <fun1(1)>" });
            CheckStructure("(<fun1(1)>,123)", new[] { "eval: <fun1(1)>", "eval: 123" });
            CheckStructure("(123,<fun1(1)>,123)", new[] { "eval: 123", "eval: <fun1(1)>", "eval: 123" });
            CheckStructure("(<tag(realm)>*(-1))", "eval: <tag(realm)>*(-1)");

            CheckStructure("(fun1)", "eval: fun1");
            CheckStructure("(fun1+1)", "eval: fun1+1");
            CheckStructure("(fun1,fun2,fun3)", new[] { "eval: fun1", "eval: fun2", "eval: fun3" });
        }

        [TestMethod]
        public void Can_parse_random_expression()
        {
            CheckStructure("({1 2})", "eval: {1 2}");
            CheckStructure("({-2 -1})", "eval: {-2 -1}");
            CheckStructure("({(1) (2)})", "eval: {(1) (2)}");
            CheckStructure("({-(1) +(2)})", "eval: {-(1) +(2)}");
            CheckStructure("({(1+1) (2+2)})", "eval: {(1+1) (2+2)}");
            CheckStructure("({-<argv(0)> <argv(0)>})", "eval: {-<argv(0)> <argv(0)>}");
            CheckStructure("({1 2}+{3 4})", "eval: {1 2}+{3 4}");
        }

        [TestMethod]
        public void Can_parse_signed_expression_arguments()
        {
            CheckStructure("(-321)", "eval: -321");
            CheckStructure("(-(1))", "eval: -(1)");
            CheckStructure("(-( 1 ))", "eval: -( 1 )");
            CheckStructure("(1+-2)", "eval: 1+-2");
            CheckStructure("(1+-(2))", "eval: 1+-(2)");
            CheckStructure("(1-(2))", "eval: 1-(2)");

            CheckStructure("(+321)", "eval: +321");
            CheckStructure("(+(1))", "eval: +(1)");
            CheckStructure("(1++2)", "eval: 1++2");
            CheckStructure("(1++(2))", "eval: 1++(2)");
            CheckStructure("(1+(2))", "eval: 1+(2)");

            CheckStructure("(1+++1,1---1)", new[] { "eval: 1+++1", "eval: 1---1" });
        }

        [TestMethod]
        public void Can_parse_expression_arguments_with_multiple_macros()
        {
            CheckStructure("(<fun1(<fun2(2)>)>)", "eval: <fun1(<fun2(2)>)>");
            CheckStructure("(<fun1(<fun2(2)>)>,123)", new[] { "eval: <fun1(<fun2(2)>)>", "eval: 123" });
            CheckStructure("(<fun1(1)>,<fun2(2)>)", new[] { "eval: <fun1(1)>", "eval: <fun2(2)>" });
        }

        [TestMethod]
        public void Can_parse_unquoted_arguments()
        {
            CheckStructure("(many words separated by space)", "unq: many words separated by space");
            CheckStructure("(many words separated by space,and other words as second argument)",
                new[] { "unq: many words separated by space", "unq: and other words as second argument" });
            CheckStructure("(#+1)", "unq: #+1");
            CheckStructure("(?!)", "unq: ?!");
        }

        [TestMethod]
        public void Can_parse_quoted_arguments()
        {
            CheckStructure("(\"some text\")", "quoted: some text");
            CheckStructure("(1,\"some text\")", new[] { "eval: 1", "quoted: some text" });
            CheckStructure("(\"some text\",1)", new[] { "quoted: some text", "eval: 1" });
            CheckStructure("(1,\"some text\",2)", new[] { "eval: 1", "quoted: some text", "eval: 2" });
            CheckStructure("(\"some text\",\"other text\")", new[] { "quoted: some text", "quoted: other text" });
            CheckStructure("(\"<hours>:<mins>:<seconds>\")", "quoted: <hours>:<mins>:<seconds>");
            CheckStructure("(\"some text with dot. and comma,\")", "quoted: some text with dot. and comma,");
            CheckStructure("(\"\")", "quoted: ");
            CheckStructure("(\"?!\")", "quoted: ?!");
            CheckStructure("(\"Natocil <?arg(u)?> <?<findres(itemdef,<tag(id)>)>.name?> z uid=<?uid?>  (<?amount?> <?name?>) umisten <?p?> (<?region.name?>)\")", "quoted: Natocil <?arg(u)?> <?<findres(itemdef,<tag(id)>)>.name?> z uid=<?uid?>  (<?amount?> <?name?>) umisten <?p?> (<?region.name?>)");
            CheckStructure("(\"<fun1(\"something\")>\")", "quoted: <fun1(\"something\")>");
        }

        [TestMethod]
        public void Can_parse_real_life_arguments()
        {
            CheckStructure("(f_statistikapovolani(<arg(dny)>))", "eval: f_statistikapovolani(<arg(dny)>)");
            CheckStructure("(\"veteran=vic jak 10. level\")", "quoted: veteran=vic jak 10. level");
            CheckStructure("(<arg(pattern)>[-0123456789])", "eval: <arg(pattern)>[-0123456789]");
        }

        [TestMethod]
        public void Can_parse_eval_arguments_with_macros()
        {
            CheckStructure("(text1<macro(1)>)", "eval: text1<macro(1)>");
            CheckStructure("(text1<macro(1)>text2)", "eval: text1<macro(1)>text2");
            CheckStructure("(text1<macro(1)>text2<macro(2)>)", "eval: text1<macro(1)>text2<macro(2)>");
            CheckStructure("(text1<macro(1)>text2,text3<macro(2)>text4)",
                new[] { "eval: text1<macro(1)>text2", "eval: text3<macro(2)>text4" });
        }

        [TestMethod]
        public void Can_parse_assignment_argument()
        {
            CheckStructure("(cont=1)", "assignment: cont=1");
        }

        private void CheckStructure(string src, params string[] expectedResults)
        {
            Parse(src, parser =>
            {
                var argumentList = parser.enclosedArgumentList();

                var extractor = new FirstLevelArgumentExtractor();
                extractor.Visit(argumentList);
                extractor.Arguments.Should().BeEquivalentTo(expectedResults);
            });
        }
    }
}
