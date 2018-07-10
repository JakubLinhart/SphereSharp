using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SphereSharp.Tests.Sphere99.Parser.ParserTestsHelper;

namespace SphereSharp.Tests.Sphere99.Parser
{
    [TestClass]
    public class StrictNatvieFunctionParsingTests
    {
        [TestMethod]
        [DataRow("action")]
        [DataRow("p")]
        [DataRow("type")]
        [DataRow("rescount")]
        public void Can_parse_expression_with_whitespace_around_operator(string memberName)
        {
            CheckExpressionStructure($"{memberName} != 1", $"operand:{memberName};operator:!=;operand:1;");
            CheckExpressionStructure($"{memberName} == 1", $"operand:{memberName};operator:==;operand:1;");
        }
    }
}
