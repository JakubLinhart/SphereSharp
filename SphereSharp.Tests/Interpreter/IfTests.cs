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
    public class IfTests
    {
        [TestMethod]
        public void Can_run_then_block()
        {
            var evaluator = new TestEvaluator();
            evaluator.SetDefault(evaluator.TestObjBase)
                .SetSrc(evaluator.TestObjBase)
                .Create();

            evaluator.EvaluateCodeBlock(@"
if (1)
    src.sysmessage true
else
    src.sysmessage false
endif");

            evaluator.TestObjBase.GetOutput().Should().Contain("sysmessage true");
        }

        [TestMethod]
        public void Can_run_else_block()
        {
            var evaluator = new TestEvaluator();
            evaluator.SetDefault(evaluator.TestObjBase)
                .SetSrc(evaluator.TestObjBase)
                .Create();

            evaluator.EvaluateCodeBlock(@"
if (0)
    src.sysmessage true
else
    src.sysmessage false
endif");

            evaluator.TestObjBase.GetOutput().Should().Contain("sysmessage false");
        }

        [TestMethod]
        public void Can_run_then_block_without_else_block()
        {
            var evaluator = new TestEvaluator();
            evaluator.SetDefault(evaluator.TestObjBase)
                .SetSrc(evaluator.TestObjBase)
                .Create();

            evaluator.EvaluateCodeBlock(@"
if (1)
    src.sysmessage true
endif");

            evaluator.TestObjBase.GetOutput().Should().Contain("sysmessage true");
        }

        [TestMethod]
        public void Can_skip_then_block_without_else_block()
        {
            var evaluator = new TestEvaluator();
            evaluator.SetDefault(evaluator.TestObjBase)
                .SetSrc(evaluator.TestObjBase)
                .Create();

            evaluator.EvaluateCodeBlock(@"
if (0)
    src.sysmessage true
endif");

            evaluator.TestObjBase.GetOutput().Should().NotContain("sysmessage true");
        }

        [TestMethod]
        public void Can_run_else_if_block()
        {
            var evaluator = new TestEvaluator();
            evaluator.SetDefault(evaluator.TestObjBase)
                .SetSrc(evaluator.TestObjBase)
                .Create();

            evaluator.EvaluateCodeBlock(@"
if (0)
    src.sysmessage true
elseif (1)
    src.sysmessage elseif
endif");

            evaluator.TestObjBase.GetOutput().Should().Contain("sysmessage elseif");
        }

        [TestMethod]
        public void Can_run_second_else_if_block()
        {
            var evaluator = new TestEvaluator();
            evaluator.SetDefault(evaluator.TestObjBase)
                .SetSrc(evaluator.TestObjBase)
                .Create();

            evaluator.EvaluateCodeBlock(@"
if (0)
    src.sysmessage true
elseif (0)
    src.sysmessage elseif
elseif (1)
    src.sysmessage second elseif
endif");

            evaluator.TestObjBase.GetOutput().Should().Contain("sysmessage second elseif");
        }

        [TestMethod]
        public void Can_skip_all_else_if_block_and_execute_else_block()
        {
            var evaluator = new TestEvaluator();
            evaluator.SetDefault(evaluator.TestObjBase)
                .SetSrc(evaluator.TestObjBase)
                .Create();

            evaluator.EvaluateCodeBlock(@"
if (0)
    src.sysmessage true
elseif (0)
    src.sysmessage elseif
elseif (0)
    src.sysmessage second elseif
else
    src.sysmessage else
endif");

            evaluator.TestObjBase.GetOutput().Should().Contain("sysmessage else");
        }
    }
}
