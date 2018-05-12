using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Sphere99.Sphere56Transpiler
{
    [TestClass]
    public class CallFunctionTests
    {
        [TestMethod]
        [DataRow("fun1", "fun1")]
        [DataRow("fun1()", "fun1")]
        [DataRow("fun1_2", "fun1_2")]
        [DataRow("fun1_2(3)", "fun1_2 3")]
        [DataRow("fun1_2(3,4,5)", "fun1_2 3,4,5")]
        [DataRow("a.b.c.fun1", "a.b.c.fun1")]
        [DataRow("a.b.c.fun1(1,2,3)", "a.b.c.fun1 1,2,3")]
        public void Can_transpile_custom_function_calls(string source, string expectedResult)
        {
            TranspileStatementCheck(source, expectedResult);
        }

        [TestMethod]
        [DataRow("arg(u,1)", "LOCAL.u=1")]
        [DataRow("arg(u,#+1)", "LOCAL.u=<LOCAL.u>+1")]
        public void Local_variables(string source, string expectedResult)
        {
            TranspileStatementCheck(source, expectedResult);
        }

        [TestMethod]
        [DataRow("arg(u,<argcount>)", "LOCAL.u=<argv>")]
        [DataRow("arg(u,<argv(0)>)", "LOCAL.u=<argv[0]>")]
        public void Arguments(string source, string expectedResult)
        {
            TranspileStatementCheck(source, expectedResult);
        }

        private void TranspileStatementCheck(string input, string expectedOutput)
        {
            var parser = new SphereSharp.Sphere99.Parser();
            var parsingOutput = parser.ParseStatement(input);

            if (parsingOutput.Errors.Any())
            {
                Assert.Fail(parsingOutput.GetErrorsText());
            }

            var transpiler = new SphereSharp.Sphere99.Sphere56Transpiler();
            transpiler.Visit(parsingOutput.Tree);

            transpiler.Output.Should().Be(expectedOutput);
        }
    }
}
