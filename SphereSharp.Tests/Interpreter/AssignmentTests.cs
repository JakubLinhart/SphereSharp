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
    public class AssignmentTests
    {
        [TestMethod]
        public void Can_assign_to_default_lvalue()
        {
            var evaluator = new TestEvaluator();
            evaluator
                .SetDefault(evaluator.TestChar)
                .Create();

            evaluator.EvaluateCodeBlock("FAME=20");

            evaluator.TestChar.Fame.Should().Be(20);
        }

        [TestMethod]
        public void Can_assign_to_tag()
        {
            var evaluator = new TestEvaluator();
            evaluator
                .SetSrc(evaluator.TestChar)
                .SetDefault(evaluator.TestChar)
                .Create();

            evaluator.EvaluateCodeBlock("tag.experience=123");

            evaluator.TestChar.Tag("experience").Should().Be("123");
        }
    }
}
