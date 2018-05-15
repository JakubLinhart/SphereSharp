using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SphereSharp.Sphere99;
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
        [DataRow("sysmessage(Zameruj jen monstra)", "sysmessage Zameruj jen monstra")]
        [DataRow("sysmessage(\"Zameruj jen monstra\")", "sysmessage \"Zameruj jen monstra\"")]
        public void Can_transpile_custom_function_calls(string src, string expectedResult)
        {
            TranspileStatementCheck(src, expectedResult);
        }

        [TestMethod]
        [DataRow("lastnew.bounce", "new.bounce")]
        [DataRow("arg(u,<Skill_Enticement.effect>)", "LOCAL.u=<serv.skill.enticement.effect>")]
        public void Name_transformation(string src, string expectedResult)
        {
            TranspileStatementCheck(src, expectedResult);
        }

        [TestMethod]
        [DataRow("morex=0", "morex=0")]
        public void Assignment(string src, string expectedResult)
        {
            TranspileStatementCheck(src, expectedResult);
        }

        [TestMethod]
        [DataRow("<fun1>", "<fun1>")]
        [DataRow("(<fun1>)", "(<fun1>)")]
        [DataRow("fun1", "<fun1>")]
        [DataRow("(fun1)", "(<fun1>)")]
        [DataRow("{1 2}", "{1 2}")]
        [DataRow("<eval 123>", "<eval 123>")]
        public void Conditions(string src, string expectedResult)
        {
            TranspileConditionCheck(src, expectedResult);
        }

        [TestMethod]
        public void If_statement()
        {
            TranspileStatementCheck(@"if 1
    call1
endif",
@"if 1
    call1
endif");

            TranspileStatementCheck(@"if 1
    if 2
        call1
    endif
endif",
@"if 1
    if 2
        call1
    endif
endif");

            TranspileStatementCheck(@"if 1
    call1
else
    call2
endif",
@"if 1
    call1
else
    call2
endif");
        }

        [TestMethod]
        [DataRow("return 1", "return 1")]
        public void Native_functions(string source, string expectedResult)
        {
            TranspileStatementCheck(source, expectedResult);
        }

        [TestMethod]
        [DataRow("arg(u,1)", "LOCAL.u=1")]
        [DataRow("arg(u,#+1)", "LOCAL.u=<LOCAL.u>+1")]
        [DataRow("arg(u,arg(v))", "LOCAL.u=LOCAL.v")]
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

        [TestMethod]
        [DataRow("tag(name,value)", "tag.name=value")]
        [DataRow("arg(u,tag(name))", "LOCAL.u=tag.name")]
        public void Tags(string source, string expectedResult)
        {
            TranspileStatementCheck(source, expectedResult);
        }

        [TestMethod]
        [DataRow("var(name,value)", "var.name=value")]
        [DataRow("arg(u,var(name))", "LOCAL.u=var.name")]
        public void Global_variables(string source, string expectedResult)
        {
            TranspileStatementCheck(source, expectedResult);
        }

        [TestMethod]
        [DataRow("findlayer(layer_pack).remove", "findlayer.layer_pack.remove")]
        [DataRow("arg(u,findlayer(layer_pack))", "LOCAL.u=findlayer layer_pack")]
        public void DottedArguments(string source, string expectedResult)
        {
            TranspileStatementCheck(source, expectedResult);
        }

        [TestMethod]
        public void Can_transpile_function()
        {
            TranspileFileCheck(
@"[function fun1]
call1
call2",

@"[function fun1]
call1
call2");
        }

        private void TranspileStatementCheck(string input, string expectedOutput)
            => TranspileCheck(input, expectedOutput, (src, parser) => parser.ParseStatement(src));

        private void TranspileFileCheck(string input, string expectedOutput) 
            => TranspileCheck(input, expectedOutput, (src, parser) => parser.ParseFile(src));

        private void TranspileConditionCheck(string input, string expectedOutput)
            => TranspileCheck(input, expectedOutput, (src, parser) => parser.ParseCondition(src));

        private void TranspileCheck(string input, string expectedOutput, Func<string, SphereSharp.Sphere99.Parser, ParsingResult> parseFunc)
        {
            var parser = new SphereSharp.Sphere99.Parser();
            var parsingOutput = parseFunc(input, parser);

            if (parsingOutput.Errors.Any())
            {
                Assert.Fail(parsingOutput.GetErrorsText());
            }

            var transpiler = new SphereSharp.Sphere99.Sphere56Transpiler();
            transpiler.Visit(parsingOutput.Tree);

            transpiler.Output.Trim().Should().Be(expectedOutput.Trim());
        }
    }
}
