using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SphereSharp.Interpreter;
using SphereSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Interpreter
{
    [TestClass]
    public class CodeBlockTests
    {
        [TestMethod]
        public void Return_terminates_code_block_execution()
        {
            var builder = new TestEvaluator();
            builder.SetArgO(builder.TestGump)
                .Create();
            builder.EvaluateCodeBlock(@"
gumppic 140 200 2200
return 1
gumppic 200 140 2200
");

            string output = builder.TestGump.GetOutput();
            output.Should().Contain("gumppic 140, 200, 2200");
            output.Should().NotContain("gumppic 200, 140, 2200");
        }

        [TestMethod]
        public void Return_returns_value()
        {
            var builder = new TestEvaluator();
            builder.Create();

            var result = builder.EvaluateCodeBlock("return 123");
            result.Should().Be("123");
        }

        [TestMethod]
        public void Can_evaluate_macro_statement()
        {
            var evaluator = new TestEvaluator();
            evaluator
                .AddNameDef("key", "src.sysmessage asdf")
                .SetSrc(evaluator.TestObjBase)
                .Create();

            evaluator.EvaluateCodeBlock("<key>");

            var output = evaluator.TestObjBase.GetOutput();
            output.Should().Contain("sysmessage asdf");
        }

        [TestMethod]
        public void Can_subscribe_events()
        {
            var events = SectionSyntax.Parse(@"[events e_something]
on=@mytrigger
src.sysmessage success - mytrigger fired
").Should().BeOfType<EventsSectionSyntax>().Which;

            var evaluator = new TestEvaluator();
            evaluator
                .SetDefault(evaluator.TestChar)
                .SetSrc(evaluator.TestObjBase)
                .AddEvents(events)
                .Create();

            evaluator.EvaluateCodeBlock(@"events +e_something");
            evaluator.TestChar.RunTrigger("mytrigger", evaluator.Context);

            evaluator.TestObjBase.GetOutput().Should().Contain("success - mytrigger fired");
        }

        [TestMethod]
        public void Can_unsubscribe_events()
        {
            var events = SectionSyntax.Parse(@"[events e_something]
on=@mytrigger
src.sysmessage success - mytrigger fired
").Should().BeOfType<EventsSectionSyntax>().Which;

            var evaluator = new TestEvaluator();
            evaluator
                .SetDefault(evaluator.TestChar)
                .SetSrc(evaluator.TestObjBase)
                .AddEvents(events)
                .Create();

            evaluator.EvaluateCodeBlock(@"events +e_something");
            evaluator.EvaluateCodeBlock(@"events -e_something");
            evaluator.TestChar.RunTrigger("mytrigger", evaluator.Context);

            evaluator.TestObjBase.GetOutput().Should().NotContain("success - mytrigger fired");
        }
    }
}
