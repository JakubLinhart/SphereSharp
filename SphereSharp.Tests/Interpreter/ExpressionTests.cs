using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SphereSharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Interpreter
{
    [TestClass]
    public class ExpressionTests
    {
        [TestMethod]
        public void Can_call_function()
        {
            string funcSrc = @"
[function myfunc]
return 123
";

            var evaluator = new TestEvaluator();
            evaluator.AddFunction(funcSrc)
                .Create();

            var result = evaluator.EvaluateExpression("myfunc");
            result.Should().Be(123);
        }

        [TestMethod]
        public void Can_negate_1()
        {
            var evaluator = new TestEvaluator().Create();
            var result = evaluator.EvaluateExpression("!1");
            result.Should().Be(0);
        }

        [TestMethod]
        public void Can_evaluate_interval()
        {
            var evaluator = new TestEvaluator().Create();
            var firstResult = evaluator.EvaluateExpression("{-2000 -3999}");
            firstResult.Should().BeGreaterOrEqualTo(-3999);
            firstResult.Should().BeLessOrEqualTo(-2000);
        }

        [TestMethod]
        public void Can_evaluate_decimal_interval()
        {
            var evaluator = new TestEvaluator().Create();
            var result = evaluator.EvaluateExpression("{3.0 4.0}");
            result.Should().BeGreaterOrEqualTo(3);
            result.Should().BeLessOrEqualTo(4);
        }

        [TestMethod]
        public void Can_evaluate_hex_number()
        {
            var evaluator = new TestEvaluator().Create();
            var result = evaluator.EvaluateExpression("0481");
            result.Should().Be(0x481);
        }

        [TestMethod]
        public void Can_evaluate_hex_number_in_defname()
        {
            var evaluator = new TestEvaluator()
                .AddNameDef("xxx", "0481")
                .Create();
            var result = evaluator.EvaluateExpression("xxx");
            result.Should().Be(0x481);
        }

        [TestMethod]
        public void Can_negate_0()
        {
            var evaluator = new TestEvaluator().Create();
            var result = evaluator.EvaluateExpression("!0");
            result.Should().Be(1);
        }

        [TestMethod]
        public void Can_add()
        {
            var evaluator = new TestEvaluator().Create();
            var result = evaluator.EvaluateExpression("5+6");
            result.Should().Be(11);
        }

        [TestMethod]
        public void Can_subtract()
        {
            var evaluator = new TestEvaluator().Create();
            var result = evaluator.EvaluateExpression("5-6");
            result.Should().Be(-1);
        }

        [TestMethod]
        public void Can_multiply()
        {
            var evaluator = new TestEvaluator().Create();
            var result = evaluator.EvaluateExpression("5*6");
            result.Should().Be(30);
        }

        [TestMethod]
        public void Can_apply_binary_or()
        {
            var evaluator = new TestEvaluator().Create();
            var result = evaluator.EvaluateExpression("2|4");
            result.Should().Be(6);
        }

        [TestMethod]
        public void Can_check_equality_of_equal_operands()
        {
            var evaluator = new TestEvaluator().Create();
            var result = evaluator.EvaluateExpression("5==5");
            result.Should().NotBe(0);
        }

        [TestMethod]
        public void Can_check_equality_of_not_equal_operands()
        {
            var evaluator = new TestEvaluator().Create();
            var result = evaluator.EvaluateExpression("5==6");
            result.Should().Be(0);
        }

        [TestMethod]
        public void Can_check_nonequality_of_equal_operands()
        {
            var evaluator = new TestEvaluator().Create();
            var result = evaluator.EvaluateExpression("5!=5");
            result.Should().Be(0);
        }

        [TestMethod]
        public void Can_check_nonequality_of_not_equal_operands()
        {
            var evaluator = new TestEvaluator().Create();
            var result = evaluator.EvaluateExpression("5!=6");
            result.Should().NotBe(0);
        }

        [TestMethod]
        public void Can_evaluate_more_than_operator_to_false_when_less()
        {
            var evaluator = new TestEvaluator().Create();
            var result = evaluator.EvaluateExpression("1>5");
            result.Should().Be(0);
        }

        [TestMethod]
        public void Can_evaluate_more_than_operator_to_false_when_equal()
        {
            var evaluator = new TestEvaluator().Create();
            var result = evaluator.EvaluateExpression("1>1");
            result.Should().Be(0);
        }

        [TestMethod]
        public void Can_evaluate_more_than_operator_to_true()
        {
            var evaluator = new TestEvaluator().Create();
            var result = evaluator.EvaluateExpression("5>1");
            result.Should().Be(1);
        }

        [TestMethod]
        public void Can_evaluate_less_than_operator_to_false_when_more()
        {
            var evaluator = new TestEvaluator().Create();
            var result = evaluator.EvaluateExpression("5<1");
            result.Should().Be(0);
        }

        [TestMethod]
        public void Can_evaluate_less_than_operator_to_false_when_equal()
        {
            var evaluator = new TestEvaluator().Create();
            var result = evaluator.EvaluateExpression("1<1");
            result.Should().Be(0);
        }

        [TestMethod]
        public void Can_evaluate_less_than_operator_to_true()
        {
            var evaluator = new TestEvaluator().Create();
            var result = evaluator.EvaluateExpression("1<5");
            result.Should().Be(1);
        }

        [TestMethod]
        public void Can_evaluate_expression_macro()
        {
            var evaluator = new TestEvaluator().Create();
            var result = evaluator.EvaluateExpression("<strlen(\"asdf\")>");

            result.Should().Be(4);
        }

        [TestMethod]
        public void Can_evaluate_expression_eval_macro()
        {
            var evaluator = new TestEvaluator().Create();
            var result = evaluator.EvaluateExpression("<eval strlen(\"asdf\")>");

            result.Should().Be(4);
        }

        [TestMethod]
        public void Can_evaluate_expression_with_namedef()
        {
            var evaluator = new TestEvaluator();
            evaluator
                .AddNameDef("test", "123")
                .Create();

            evaluator.EvaluateExpression("test==123").Should().NotBe(0);
        }

        [TestMethod]
        public void Can_evaluate_expression_with_skilldef()
        {
            var evaluator = new TestEvaluator();
            evaluator
                .AddSkillDef(new SkillDef()
                {
                    DefName = "skill_meditation",
                    Id = 123,
                })
                .Create();

            evaluator.EvaluateExpression("skill_meditation==123").Should().NotBe(0);
        }
    }
}
