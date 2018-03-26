using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SphereSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Interpreter
{
    [TestClass]
    public class ArgumentTests
    {
        [TestMethod]
        public void Can_pass_safe_literal_with_macro()
        {
            var builder = new TestEvaluator();
            builder.AddNameDef("seznamclass[0]", "asdf")
                .SetArgO(builder.TestGump)
                .Create();

            builder.EvaluateCodeBlock(@"HTMLGUMPa(365,215,110,160,<?safe?><?seznamclass[0]?>,0,0)");
            var output = builder.TestGump.GetOutput();
            output.Should().Contain("htmlgumpa 365, 215, 110, 160, \"asdf\"");
        }

        [TestMethod]
        public void Can_pass_literal_with_eval_macro()
        {
            var builder = new TestEvaluator();
            builder
                .SetSrc(builder.TestObjBase)
                .AddNameDef("seznamclass[0]", "1")
                .AddNameDef("seznamclass[1]", "2")
                .Create();

            builder.EvaluateCall(@"src.sysmessage(""<eval seznamclass[0]+seznamclass[1]>"")");
            builder.TestObjBase.GetOutput().Should().Contain("sysmessage 3");
        }

        [TestMethod]
        public void Can_eval_expression()
        {
            var evaluator = new TestEvaluator();
            evaluator
                .SetDefault(evaluator.TestObjBase)
                .SetSrc(evaluator.TestObjBase)
                .SetArgO(evaluator.TestGump)
                .Create();

            evaluator.EvaluateCodeBlock(@"
tag(basestr, 200)
argo.settext(10,<eval tag(basestr)>)");

            var output = evaluator.TestGump.GetOutput();
            output.Should().Contain("settext 10, 200");
        }
    }
}
