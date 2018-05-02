using Antlr4.Runtime.Misc;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Parser.Sphere99
{
    [TestClass]
    public class EvalExpressionTests : ParsingTestBase
    {
        [TestMethod]
        public void Can_parse_expression_with_whitespace_around_operator()
        {
            RoundtripCheck("1 - 1");
            RoundtripCheck("1 * 1");
            RoundtripCheck("1 == 1");
            RoundtripCheck("1 != 1");
            RoundtripCheck("1 > 1");
            RoundtripCheck("1 < 1");
            RoundtripCheck("1 >= 1");
            RoundtripCheck("1 <= 1");
        }

        [TestMethod]
        public void Can_parse_hex_numbers()
        {
            RoundtripCheck("0dead");
            RoundtripCheck("0dead-0beef");
        }

        [TestMethod]
        public void Can_parse_basic_expression()
        {
            RoundtripCheck("1");
            RoundtripCheck("1+1");
            RoundtripCheck("1-1");
            RoundtripCheck("1*1");
            RoundtripCheck("1/1");
            RoundtripCheck("1==1");
            RoundtripCheck("1!=1");
            RoundtripCheck("1>1");
            RoundtripCheck("1<1");
            RoundtripCheck("1>=1");
            RoundtripCheck("1<=1");
            RoundtripCheck("1&&1");
            RoundtripCheck("1||1");
            RoundtripCheck("1&1");
            RoundtripCheck("1|1");
            RoundtripCheck("1%1");

            RoundtripCheck("-1");
            RoundtripCheck("+1");
            RoundtripCheck("~1");
            RoundtripCheck("!1");

            RoundtripCheck("1+-1");
            RoundtripCheck("1+-+-+-+1");

            RoundtripCheck("1+1+1");

            RoundtripCheck("(1)");
            RoundtripCheck("(-1)");
            RoundtripCheck("-(1)");
            RoundtripCheck("(1+1)");
            RoundtripCheck("-(1+1)");
            RoundtripCheck("(1+1+1)");
            RoundtripCheck("+(1+1+1)");
            RoundtripCheck("1+(2+3)+(4+5+6)");
            RoundtripCheck("((1+(2+3))+(4+5+6))*1");

            RoundtripCheck("<fun1>");
            RoundtripCheck("<fun1>+1");
            RoundtripCheck("1+<fun1>");
            RoundtripCheck("<fun1>+<fun2>");
            RoundtripCheck("(<fun1>+<fun2>)+<fun3>");

            RoundtripCheck("<fun1(1)>");
            RoundtripCheck("<fun1(1)>+1");
            RoundtripCheck("1+<fun1(1)>");
            RoundtripCheck("<fun1(1)>+<fun2(2)>");
            RoundtripCheck("<fun1(1)>+<fun2(2)>+<fun3(3)>");
            RoundtripCheck("(<fun1(1)>+<fun2(2)>)+<fun3(3)>");
            RoundtripCheck("<fun1(1)>+(<fun2(2)>+<fun3(3)>)");
        }

        [TestMethod]
        public void Can_parse_range_expression()
        {
            RoundtripCheck("{1 2}");
            RoundtripCheck("{-2 -1}");
            RoundtripCheck("{(1) (2)}");
            RoundtripCheck("{-(1) +(2)}");
            RoundtripCheck("{(1+1) (2+2)}");
            RoundtripCheck("{-<argv(0)> <argv(0)>}");
            RoundtripCheck("{1 2} + {3 4}");
        }

        [TestMethod]
        public void Can_parse_less_than_more_than_madness()
        {
            RoundtripCheck("1<2>3");
            RoundtripCheck("<fun1><2><fun2>");
        }

        [TestMethod]
        public void Can_parse_expressions_with_direct_calls()
        {
            RoundtripCheck("fun1(1)");
            RoundtripCheck("1+fun1(1)");
            RoundtripCheck("-fun1(1)");
            RoundtripCheck("-(fun1(1))");
            RoundtripCheck("fun1(1)+1");
            RoundtripCheck("fun1(1)+fun2(2)");
            RoundtripCheck("fun1(1)+fun2(2)+fun3(3)");
            RoundtripCheck("(fun1(1)+fun2(2))");
            RoundtripCheck("(fun1(1)+fun2(2))");
        }

        private void RoundtripCheck(string src)
        {
            Parse(src, parser =>
            {
                var expression = parser.evalExpression();
                var generator = new EvalExpressionGenerator();
                generator.Visit(expression);

                generator.Result.Should().Be(src);
            });
        }

        private class EvalExpressionGenerator : sphereScript99BaseVisitor<bool>
        {
            private StringBuilder result = new StringBuilder();

            public string Result => result.ToString();

            public override bool VisitEvalOperator([NotNull] sphereScript99Parser.EvalOperatorContext context)
            {
                result.Append(context.GetText());

                return base.VisitEvalOperator(context);
            }

            public override bool VisitConstantExpression([NotNull] sphereScript99Parser.ConstantExpressionContext context)
            {
                result.Append(context.GetText());

                return base.VisitConstantExpression(context);
            }

            public override bool VisitEvalSubExpression([NotNull] sphereScript99Parser.EvalSubExpressionContext context)
            {
                result.Append('(');

                var ret = base.VisitEvalSubExpression(context);

                result.Append(')');

                return ret;
            }

            public override bool VisitFirstMemberAccess([NotNull] sphereScript99Parser.FirstMemberAccessContext context)
            {
                result.Append(context.GetText());

                return true;
            }

            public override bool VisitMemberAccess([NotNull] sphereScript99Parser.MemberAccessContext context)
            {
                result.Append(context.GetText());

                return true;
            }

            public override bool VisitMacro([NotNull] sphereScript99Parser.MacroContext context)
            {
                result.Append('<');
                var ret = base.VisitMacro(context);
                result.Append('>');

                return ret;
            }

            public override bool VisitSignedEvalOperand([NotNull] sphereScript99Parser.SignedEvalOperandContext context)
            {
                result.Append(context.unaryOperator()?.GetText() ?? string.Empty);

                return base.VisitSignedEvalOperand(context);
            }

            public override bool VisitRangeExpression([NotNull] sphereScript99Parser.RangeExpressionContext context)
            {
                result.Append('{');
                Visit(context.evalExpression()[0]);
                result.Append(' ');
                Visit(context.evalExpression()[1]);
                result.Append('}');

                return false;
            }
        }
    }
}
