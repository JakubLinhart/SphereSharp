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
            CheckStructure("fun1(321)", "arg: 321");
            CheckStructure("fun1(0abc)", "arg: 0abc");
            CheckStructure("fun1(#0abc)", "arg: #0abc");
            CheckStructure("fun1(123,321)", new[] { "arg: 123", "arg: 321" });
            CheckStructure("fun1(123,321,987)", new[] { "arg: 123", "arg: 321", "arg: 987" });
            CheckStructure("fun1(123, 321, 987)", new[] { "arg: 123", "arg: 321", "arg: 987" });
            CheckStructure("fun1(123 ,321 ,987)", new[] { "arg: 123", "arg: 321", "arg: 987" });
            CheckStructure("fun1(1,)", "arg: 1", "empty");

            CheckStructure("fun1(1+1)", "arg: 1+1");
            CheckStructure("fun1(1-1)", "arg: 1-1");
            CheckStructure("fun1(1*1)", "arg: 1*1");
            CheckStructure("fun1(1/1)", "arg: 1/1");
            CheckStructure("fun1(1&1)", "arg: 1&1");
            CheckStructure("fun1(1|1)", "arg: 1|1");
            CheckStructure("fun1(1%1)", "arg: 1%1");

            CheckStructure("fun1(-1)", "arg: -1");
            CheckStructure("fun1(+1)", "arg: +1");
            CheckStructure("fun1(~1)", "arg: ~1");
            CheckStructure("fun1(!1)", "arg: !1");

            CheckStructure("fun1(1+1+1)", "arg: 1+1+1");

            CheckStructure("fun1((1))", "arg: (1)");
            CheckStructure("fun1(((1)))", "arg: ((1))");
            CheckStructure("fun1((1+1))", "arg: (1+1)");
            CheckStructure("fun1((1+1)+1)", "arg: (1+1)+1");
            CheckStructure("fun1((1+1)+(1+1))", "arg: (1+1)+(1+1)");
            CheckStructure("fun1((1+(1+1))+(1+1))", "arg: (1+(1+1))+(1+1)");
            CheckStructure("fun1((1+(1+1))+(1+1),(2+(2+2))+(2+2))", new[] { "arg: (1+(1+1))+(1+1)", "arg: (2+(2+2))+(2+2)" });
            CheckStructure("fun1((1+1),(2+2),(3+3))", new[] { "arg: (1+1)", "arg: (2+2)", "arg: (3+3)" });

            CheckStructure("fun1(123,<fun1(1)>)", new[] { "arg: 123", "arg: <fun1(1)>" });
            CheckStructure("fun1(<fun1(1)>,123)", new[] { "arg: <fun1(1)>", "arg: 123" });
            CheckStructure("fun1(123,<fun1(1)>,123)", new[] { "arg: 123", "arg: <fun1(1)>", "arg: 123" });
            CheckStructure("fun1(<tag(realm)>*(-1))", "arg: <tag(realm)>*(-1)");

            CheckStructure("fun1(fun1)", "unq: fun1");
            CheckStructure("fun1(fun1+1)", "unq: fun1+1");
            CheckStructure("fun1(fun1,fun2,fun3)", new[] { "unq: fun1", "unq: fun2", "unq: fun3" });
        }

        [TestMethod]
        public void Parses_random_expression_as_unquoted_literal()
        {
            CheckStructure("fun1({1 2})", "unq: {1 2}");
        }

        [TestMethod]
        public void Can_parse_signed_expression_arguments()
        {
            CheckStructure("fun1(-321)", "arg: -321");
            CheckStructure("fun1(-(1))", "arg: -(1)");
            CheckStructure("fun1(-( 1 ))", "arg: -( 1 )");
            CheckStructure("fun1(1+-2)", "arg: 1+-2");
            CheckStructure("fun1(1+-(2))", "arg: 1+-(2)");
            CheckStructure("fun1(1-(2))", "arg: 1-(2)");

            CheckStructure("fun1(+321)", "arg: +321");
            CheckStructure("fun1(+(1))", "arg: +(1)");
            CheckStructure("fun1(1++2)", "arg: 1++2");
            CheckStructure("fun1(1++(2))", "arg: 1++(2)");
            CheckStructure("fun1(1+(2))", "arg: 1+(2)");

            CheckStructure("fun1(1+++1,1---1)", new[] { "arg: 1+++1", "arg: 1---1" });
        }

        [TestMethod]
        public void Can_parse_expression_arguments_with_multiple_macros()
        {
            CheckStructure("fun1(<fun1(<fun2(2)>)>)", "arg: <fun1(<fun2(2)>)>");
            CheckStructure("fun1(<fun1(<fun2(2)>)>,123)", new[] { "arg: <fun1(<fun2(2)>)>", "arg: 123" });
            CheckStructure("fun1(<fun1(1)>,<fun2(2)>)", new[] { "arg: <fun1(1)>", "arg: <fun2(2)>" });
        }

        [TestMethod]
        public void Can_parse_unquoted_arguments()
        {
            CheckStructure("fun1(many words separated by space)", "unq: many words separated by space");
            CheckStructure("fun1(many words separated by space,and other words as second argument)",
                new[] { "unq: many words separated by space", "unq: and other words as second argument" });
            CheckStructure("fun1(some text with (parenthesis))", "unq: some text with (parenthesis)");
            CheckStructure("fun1(some text (with) (parenthesis))", "unq: some text (with) (parenthesis)");
            CheckStructure("fun1(some text (with) ((nested)) (parenthesis))", "unq: some text (with) ((nested)) (parenthesis)");
            CheckStructure("fun1(some text with () empty parenthesis)", "unq: some text with () empty parenthesis");
            CheckStructure("fun1(some text with (<fun2>) macro in parenthesis)", "unq: some text with (<fun2>) macro in parenthesis");
            CheckStructure("fun1(?!)", "unq: ?!");
            CheckStructure("fun1(<src.sex a> a b c <name>)", "unq: <src.sex a> a b c <name>");
            CheckStructure("fun1(<src.sex(ab)> this cannot be an expression <src.name>)", "unq: <src.sex(ab)> this cannot be an expression <src.name>");
            CheckStructure("sysmessage Some text <?fun1?>some text with <BASEFONT SIZE=\"+5\" COLOR=\"#000080\">html</BASEFONT> tags",
                "unq: Some text <?fun1?>some text with <BASEFONT SIZE=\"+5\" COLOR=\"#000080\">html</BASEFONT> tags");
            CheckStructure("sysmessage(Some text <?fun1?>some text with <BASEFONT SIZE=\"+5\" COLOR=\"#000080\">html</BASEFONT> tags)",
                "unq: Some text <?fun1?>some text with <BASEFONT SIZE=\"+5\" COLOR=\"#000080\">html</BASEFONT> tags");
            CheckStructure("sysmessage(<basefont color=#ffff88> sipe <basefont color=#ffffff>)",
                "unq: <basefont color=#ffff88> sipe <basefont color=#ffffff>");
        }

        [TestMethod]
        public void Can_parse_quoted_arguments()
        {
            CheckStructure("fun1(\"can contain ''\")", "quoted: can contain ''");
            CheckStructure("fun1(\"can contain \\,:.;?!*/-+][()}{\")", "quoted: can contain \\,:.;?!*/-+][()}{");
            CheckStructure("fun1(\"*<*\")", "quoted: *<*");
            CheckStructure("fun1(\"some text\")", "quoted: some text");
            CheckStructure("fun1(1,\"some text\")", new[] { "arg: 1", "quoted: some text" });
            CheckStructure("fun1(\"some text\",1)", new[] { "quoted: some text", "arg: 1" });
            CheckStructure("fun1(1,\"some text\",2)", new[] { "arg: 1", "quoted: some text", "arg: 2" });
            CheckStructure("fun1(\"some text\",\"other text\")", new[] { "quoted: some text", "quoted: other text" });
            CheckStructure("fun1(\"<hours>:<mins>:<seconds>\")", "quoted: <hours>:<mins>:<seconds>");
            CheckStructure("fun1(\"some text with dot. and comma,\")", "quoted: some text with dot. and comma,");
            CheckStructure("fun1(\"\")", "quoted: ");
            CheckStructure("fun1(\"Natocil <?arg(u)?> <?<findres(itemdef,<tag(id)>)>.name?> z uid=<?uid?>  (<?amount?> <?name?>) umisten <?p?> (<?region.name?>)\")",
                "quoted: Natocil <?arg(u)?> <?<findres(itemdef,<tag(id)>)>.name?> z uid=<?uid?>  (<?amount?> <?name?>) umisten <?p?> (<?region.name?>)");
            CheckStructure("fun1(\"<fun1(\"something\")>\")", "quoted: <fun1(\"something\")>");
            CheckStructure("fun1(\"Some text <?fun1?>some text with <BASEFONT SIZE=\"+5\" COLOR=\"#000080\">html</BASEFONT> tags\")",
                "quoted: Some text <?fun1?>some text with <BASEFONT SIZE=\"+5\" COLOR=\"#000080\">html</BASEFONT> tags");
            CheckStructure("x(\"<basefont color=#ffff88>* sipe *<basefont color=#ffffff>\")",
                "quoted: <basefont color=#ffff88>* sipe *<basefont color=#ffffff>");

        }

        [TestMethod]
        public void Can_parse_real_life_arguments()
        {
            CheckStructure("fun1(f_statistikapovolani(<arg(dny)>))", "unq: f_statistikapovolani(<arg(dny)>)");
            CheckStructure("fun1(\"veteran=vic jak 10. level\")", "quoted: veteran=vic jak 10. level");
            CheckStructure("fun1(<arg(pattern)>[-0123456789])", "unq: <arg(pattern)>[-0123456789]");
        }

        [TestMethod]
        public void Can_parse_eval_arguments_with_macros()
        {
            CheckStructure("fun1(text1<macro(1)>)", "unq: text1<macro(1)>");
            CheckStructure("fun1(text1<macro(1)>text2)", "unq: text1<macro(1)>text2");
            CheckStructure("fun1(text1<macro(1)>text2<macro(2)>)", "unq: text1<macro(1)>text2<macro(2)>");
            CheckStructure("fun1(text1<macro(1)>text2,text3<macro(2)>text4)",
                new[] { "unq: text1<macro(1)>text2", "unq: text3<macro(2)>text4" });
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
