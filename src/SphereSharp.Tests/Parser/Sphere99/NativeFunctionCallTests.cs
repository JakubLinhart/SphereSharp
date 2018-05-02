using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Parser.Sphere99
{
    [TestClass]
    public class NativeFunctionCallTests : ParsingTestBase
    {
        [TestMethod]
        public void Can_parse_basic_calls()
        {
            CheckStructure("sysmessage asdf qwer sadf zxcv", "unq: asdf qwer sadf zxcv");
            CheckStructure("sysmessage(asdf qwer sadf zxcv)", "unq: asdf qwer sadf zxcv");
            CheckStructure("sysmessage(\"asdf qwer sadf zxcv\")", "quoted: asdf qwer sadf zxcv");
            CheckStructure("sysmessage 1+1", "expr: 1+1");
            CheckStructure("sysmessage(1+1)", "expr: 1+1");
            CheckStructure("src.sysmessage(1+1)", "expr: 1+1");

            CheckStructure("return 1", "expr: 1");
            CheckStructure("arrowquest 0", "expr: 0");
            CheckStructure("timer 100", "expr: 100");
            CheckStructure("consume 123", "expr: 123");
            CheckStructure("consume(1000 t_custom_spell)", "unq: 1000 t_custom_spell");
        }

        [TestMethod]
        public void Can_parse_events_calls()
        {
            CheckStructure("events(-e_spelleffect)", "event: -e_spelleffect");
            CheckStructure("events(+e_spelleffect)", "event: +e_spelleffect");
            CheckStructure("events(e_spelleffect)", "unq: e_spelleffect");
            CheckStructure("events -e_spelleffect", "event: -e_spelleffect");
            CheckStructure("events +e_spelleffect", "event: +e_spelleffect");
            CheckStructure("events e_spelleffect", "unq: e_spelleffect");
        }

        [TestMethod]
        public void Can_parse_trigger_calls()
        {
            CheckStructure("trigger @userclick", "trigger: userclick");
            CheckStructure("trigger(@dropon_ground)", "trigger: dropon_ground");
        }

        [TestMethod]
        public void Can_parse_eval_calls()
        {
            CheckStructure("eval 1+1", "eval: 1+1");
            CheckStructure("hval 1+1", "eval: 1+1");
            CheckStructure("safe fun1(1)", "eval: fun1(1)");
            CheckStructure("safe(fun1(1))", "eval: (fun1(1))");
            CheckStructure("hval fun1(1)+fun2(2)", "eval: fun1(1)+fun2(2)");
        }

        [TestMethod]
        public void Can_parse_arg_calls()
        {
            CheckStructure("arg(u,#+1)", new[] { "unq: u", "unq: #+1" });
        }

        private void ShouldSucceed(string src) => Parse(src, parser => parser.call());

        private void CheckStructure(string src, params string[] expectedResults)
        {
            Parse(src, parser =>
            {
                var argumentList = parser.call();

                var extractor = new FirstLevelArgumentExtractor();
                extractor.Visit(argumentList);
                extractor.Arguments.Should().BeEquivalentTo(expectedResults);
            });
        }
    }
}
