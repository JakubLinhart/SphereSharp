using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Interpreter
{
    [TestClass]
    public class DoSwitchTests
    {
        [TestMethod]
        public void Can_evaluate_doswitch()
        {
            var evaluator = new TestEvaluator();
            evaluator.SetSrc(evaluator.TestObjBase)
                .SetDefault(evaluator.TestObjBase)
                .Create();

            evaluator.EvaluateCodeBlock(@"doswitch 1
    src.sysmessage 1
    src.sysmessage 2
    src.sysmessage 3
enddo
");

            evaluator.TestObjBase.GetOutput().Should().NotContain("sysmessage 1");
            evaluator.TestObjBase.GetOutput().Should().Contain("sysmessage 2");
            evaluator.TestObjBase.GetOutput().Should().NotContain("sysmessage 3");
        }
    }
}
