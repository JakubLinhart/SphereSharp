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
    public class BuiltInPropertyBindingsTests
    {
        [TestMethod]
        public void Can_assign_item_properties()
        {
            var evaluator = new TestEvaluator();
            evaluator
                .SetDefault(evaluator.TestItem)
                .Create();

            evaluator.EvaluateCodeBlock("color=0481");
            evaluator.TestItem.Color.Should().Be(0x481);
        }

        [TestMethod]
        public void Can_assign_char_properties()
        {
            var evaluator = new TestEvaluator();
            evaluator
                .SetDefault(evaluator.TestChar)
                .Create();

            evaluator.EvaluateCodeBlock("FAME=1");
            evaluator.TestChar.Fame.Should().Be(1);

            evaluator.EvaluateCodeBlock("NPC=123");
            evaluator.TestChar.Npc.Should().Be(123);

            evaluator.EvaluateCodeBlock("KARMA=2");
            evaluator.TestChar.Karma.Should().Be(2);

            evaluator.EvaluateCodeBlock("maxhits=3");
            evaluator.TestChar.MaxHits.Should().Be(3);

            evaluator.EvaluateCodeBlock("maxstam=4");
            evaluator.TestChar.MaxStam.Should().Be(4);
            evaluator.EvaluateCodeBlock("maxmana=5");
            evaluator.TestChar.MaxMana.Should().Be(5);

            evaluator.EvaluateCodeBlock("STR=6");
            evaluator.TestChar.Str.Should().Be(6);
            evaluator.EvaluateCodeBlock("DEX=7");
            evaluator.TestChar.Dex.Should().Be(7);
            evaluator.EvaluateCodeBlock("INT=8");
            evaluator.TestChar.Int.Should().Be(8);

            evaluator.EvaluateCodeBlock("Parrying=9");
            evaluator.TestChar.Parrying.Should().Be(9);
            evaluator.EvaluateCodeBlock("Tactics=10");
            evaluator.TestChar.Tactics.Should().Be(10);
            evaluator.EvaluateCodeBlock("Wrestling=11");
            evaluator.TestChar.Wrestling.Should().Be(11);
            evaluator.EvaluateCodeBlock("SpiritSpeak=12");
            evaluator.TestChar.SpiritSpeak.Should().Be(12);

            evaluator.EvaluateCodeBlock("color=0481");
            evaluator.TestChar.Color.Should().Be(0x481);
        }
    }
}
