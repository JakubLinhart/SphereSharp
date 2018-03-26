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
    public class SymbolTests
    {
        [TestMethod]
        public void Can_evaluate_symbol_with_one_text_segment()
        {
            var evaluator = new TestEvaluator();
            evaluator.Create();

            var result = evaluator.EvaluateSymbol("segment1");
            result.Should().Be("segment1");
        }

        [TestMethod]
        public void Can_evaluate_symbol_with_multiple_segments()
        {
            var evaluator = new TestEvaluator();
            evaluator
                .AddNameDef("defname1", "segment2")
                .AddNameDef("defname2", "segment3")
                .Create();

            var result = evaluator.EvaluateSymbol("segment1_<defname1>_<defname2>");
            result.Should().Be("segment1_segment2_segment3");
        }

        [TestMethod]
        public void Can_evaluate_indexed_symbol()
        {
            var evaluator = new TestEvaluator();
            evaluator.Create();

            var result = evaluator.EvaluateSymbol("defname[1]");
            result.Should().Be("defname[1]");
        }

        [TestMethod]
        public void Can_evaluate_indexed_symbol_with_expression_index()
        {
            var evaluator = new TestEvaluator();
            evaluator.Create();

            var result = evaluator.EvaluateSymbol("defname[8*8]");
            result.Should().Be("defname[64]");
        }
    }
}
