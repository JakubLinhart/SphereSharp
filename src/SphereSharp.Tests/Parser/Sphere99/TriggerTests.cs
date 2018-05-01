using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Parser.Sphere99
{
    [TestClass]
    public class TriggerTests : ParsingTestBase
    {
        [TestMethod]
        public void Can_parse_trigger_with_one_statement()
        {
            StructureCheck("UserDClick:1;", @"on=@UserDClick
return 1");
        }

        [TestMethod]
        public void Can_parse_trigger_with_comments_and_first_empty_line()
        {
            StructureCheck("UserDClick:1;", @"on=@UserDClick // some comment

// empty line before this comment is important for this test
return 1
// last line with comment");
        }

        [TestMethod]
        public void Can_parse_trigger_with_two_statements()
        {
            StructureCheck("timer:2;", @"on=@timer
call1
return 1");

        }

        [TestMethod]
        public void Can_parse_trigger_without_statement()
        {
            StructureCheck("userdclick:0;", @"on=@userdclick");
        }

        private void StructureCheck(string expectedResult, string src)
        {
            sphereScript99Parser.TriggerContext trigger = null;

            Parse(src, parser =>
            {
                trigger = parser.trigger();
            });

            var extractor = new TriggerCodeBlockExtractor();
            extractor.Visit(trigger);

            extractor.Output.Should().Be(expectedResult);
        }
    }
}
