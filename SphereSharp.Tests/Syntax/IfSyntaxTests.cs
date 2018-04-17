using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SphereSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Syntax
{
    [TestClass]
    public class IfSyntaxTests
    {
        [TestMethod]
        public void Can_parse_if_without_else()
        {
            var syntax = IfSyntax.Parse(@"if (1==1)
    func1
endif
");
            syntax.Condition.As<BinaryOperatorSyntax>().Operator.Should().Be(BinaryOperatorKind.Equal);
            syntax.ThenBlock.Statements.Length.Should().Be(1);
            syntax.ThenBlock.Statements[0].As<CallSyntax>().MemberName.Should().Be("func1");
            syntax.ElseBlock.Should().Be(CodeBlockSyntax.Empty);
        }

        [TestMethod]
        public void Can_parse_if_with_else()
        {
            var syntax = IfSyntax.Parse(@"if (1==1)
    func1
else
    func2
endif
");
            syntax.Condition.As<BinaryOperatorSyntax>().Operator.Should().Be(BinaryOperatorKind.Equal);
            syntax.ThenBlock.Statements.Length.Should().Be(1);
            syntax.ThenBlock.Statements[0].As<CallSyntax>().MemberName.Should().Be("func1");
            syntax.ElseBlock.Statements.Length.Should().Be(1);
            syntax.ElseBlock.Statements[0].As<CallSyntax>().MemberName.Should().Be("func2");
        }

        [TestMethod]
        public void Can_parse_if_with_one_elseif()
        {
            var syntax = IfSyntax.Parse(@"if (1==1)
    func1
elseif (1==1)
    func2
endif
");

            syntax.ThenBlock.Statements[0].As<CallSyntax>().MemberName.Should().Be("func1");
            syntax.ElseIfs.Length.Should().Be(1);
            syntax.ElseIfs[0].Condition.As<BinaryOperatorSyntax>().Operator.Should().Be(BinaryOperatorKind.Equal);
            syntax.ElseIfs[0].ThenBlock.Statements[0].As<CallSyntax>().MemberName.Should().Be("func2");
            syntax.ElseBlock.Should().Be(CodeBlockSyntax.Empty);
        }

        [TestMethod]
        public void Can_parse_if_with_empty_elseif()
        {
            var syntax = IfSyntax.Parse(@"if (1==1)
    func1
elseif (1==1)
endif
");

            syntax.ThenBlock.Statements[0].As<CallSyntax>().MemberName.Should().Be("func1");
            syntax.ElseIfs.Length.Should().Be(1);
            syntax.ElseIfs[0].Condition.As<BinaryOperatorSyntax>().Operator.Should().Be(BinaryOperatorKind.Equal);
            syntax.ElseIfs[0].ThenBlock.Statements.Should().HaveCount(0);
            syntax.ElseBlock.Should().Be(CodeBlockSyntax.Empty);
        }

        [TestMethod]
        public void Can_parse_if_with_empty_then()
        {
            var syntax = IfSyntax.Parse(@"if (1==1)
endif
");

            syntax.ThenBlock.Statements.Should().HaveCount(0);
        }

        [TestMethod]
        public void Can_parse_if_with_multiple_elseifs()
        {
            var syntax = IfSyntax.Parse(@"if (1==1)
    func1
elseif (1==1)
    func2
elseif (2!=2)
    func3
elseif (3||4)
    func4
endif
");

            syntax.ThenBlock.Statements[0].As<CallSyntax>().MemberName.Should().Be("func1");
            syntax.ElseIfs.Length.Should().Be(3);
            syntax.ElseIfs[0].Condition.As<BinaryOperatorSyntax>().Operator.Should().Be(BinaryOperatorKind.Equal);
            syntax.ElseIfs[0].ThenBlock.Statements[0].As<CallSyntax>().MemberName.Should().Be("func2"); syntax.ElseIfs[0].Condition.As<BinaryOperatorSyntax>().Operator.Should().Be(BinaryOperatorKind.Equal);
            syntax.ElseIfs[1].Condition.As<BinaryOperatorSyntax>().Operator.Should().Be(BinaryOperatorKind.NotEqual);
            syntax.ElseIfs[1].ThenBlock.Statements[0].As<CallSyntax>().MemberName.Should().Be("func3");
            syntax.ElseIfs[2].Condition.As<BinaryOperatorSyntax>().Operator.Should().Be(BinaryOperatorKind.LogicalOr);
            syntax.ElseIfs[2].ThenBlock.Statements[0].As<CallSyntax>().MemberName.Should().Be("func4");
            syntax.ElseBlock.Should().Be(CodeBlockSyntax.Empty);
        }

        [TestMethod]
        public void Can_parse_if_with_one_elseif_and_else()
        {
            var syntax = IfSyntax.Parse(@"if (1==1)
    func1
elseif (1==1)
    func2
else
    func3
endif
");

            syntax.ThenBlock.Statements[0].As<CallSyntax>().MemberName.Should().Be("func1");
            syntax.ElseIfs.Length.Should().Be(1);
            syntax.ElseIfs[0].Condition.As<BinaryOperatorSyntax>().Operator.Should().Be(BinaryOperatorKind.Equal);
            syntax.ElseIfs[0].ThenBlock.Statements[0].As<CallSyntax>().MemberName.Should().Be("func2");
            syntax.ElseBlock.Statements.Length.Should().Be(1);
            syntax.ElseBlock.Statements[0].As<CallSyntax>().MemberName.Should().Be("func3");
        }

        [TestMethod]
        public void Can_parse_if_multiple_statements_in_all_blocks()
        {
            var syntax = IfSyntax.Parse(@"if (1==1)
    func1
    func2
elseif (1==1)
    func3
    func4
    func5
else
    func6
    func7
    func8
    func9
endif
");

            syntax.ThenBlock.Statements.Length.Should().Be(2);
            syntax.ElseIfs.Length.Should().Be(1);
            syntax.ElseIfs[0].ThenBlock.Statements.Length.Should().Be(3);
            syntax.ElseBlock.Statements.Length.Should().Be(4);
        }

        [TestMethod]
        public void Can_parse_nested_ifs()
        {
            var syntax = IfSyntax.Parse(@"if (1==1)
    if (1==1)
        func1
    elseif (2==2)
        func2
    else
        func3
    endif
elseif (3==3)
    if (4==4)
        func4
    endif
else
    if (1==1)
        func4
    endif
endif
");
            syntax.ThenBlock.Statements[0].As<IfSyntax>().Should().NotBeNull();
            syntax.ElseBlock.Statements[0].As<IfSyntax>().Should().NotBeNull();
            syntax.ElseIfs[0].ThenBlock.Statements[0].As<IfSyntax>().Should().NotBeNull();
        }

        [TestMethod]
        public void Can_parse_condition_with_nested_macro_expression()
        {
            var syntax = IfSyntax.Parse(@"if (<strlen(<tag(nation)>)>)
    call1
endif");

            syntax.Condition.As<MacroExpressionSyntax>().Macro.Call.MemberName.Should().Be("strlen");
        }

        [TestMethod]
        public void Can_parse_condition_with_call()
        {
            var syntax = IfSyntax.Parse(@"if (strlen(1))
    call1
endif");

            syntax.ThenBlock.Statements.Should().HaveCount(1);
            syntax.ThenBlock.Statements[0].Should().BeOfType<CallSyntax>();
        }

        [TestMethod]
        public void Can_parse_condition_with_logical_not_operator_in_front_of_parentheses()
        {
            var syntax = IfSyntax.Parse(@"if !(1)
    call1
endif");

            syntax.Condition.Should().BeOfType<UnaryOperatorSyntax>().Which.Kind.Should().Be(UnaryOperatorKind.LogicalNot);
        }

        [TestMethod]
        public void Can_parse_if_endif_else_followed_by_comment()
        {
            var syntax = IfSyntax.Parse(@"if (1) // comment
    call1
else // comment
    call2
endif // comment");
            syntax.Should().NotBeNull();
            syntax.ThenBlock.Statements.Should().HaveCount(1);
            syntax.ElseBlock.Statements.Should().HaveCount(1);
        }

        [TestMethod]
        public void Can_parse_nested_if_with_whitespace_and_comment_at_the_end_of_endif_line()
        {
            var syntax = IfSyntax.Parse(@"if (1)
    if (2==2)
        call2
    endif  
endif");

            syntax.ThenBlock.Statements.Should().HaveCount(1);
            syntax.ThenBlock.Statements[0].Should().BeOfType<IfSyntax>();
        }

        [TestMethod]
        public void Can_parse_return_statement_with_macro_inside_then_and_else_blocks()
        {
            var syntax = IfSyntax.Parse(@"if 1==1
    return <args>
else
    return <args>
endif");

            syntax.ThenBlock.Statements.Should().HaveCount(1);
            syntax.ThenBlock.Statements[0].Should().BeOfType<ReturnSyntax>();
            syntax.ElseBlock.Statements.Should().HaveCount(1);
            syntax.ElseBlock.Statements[0].Should().BeOfType<ReturnSyntax>();
        }
    }
}