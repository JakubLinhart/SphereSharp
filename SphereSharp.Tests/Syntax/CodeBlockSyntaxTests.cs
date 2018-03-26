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
    public class CodeBlockSyntaxTests
    {
        [TestMethod]
        public void Can_parse_code_block_with_one_statement()
        {
            var syntax = CodeBlockSyntax.Parse("method_call");

            syntax.Statements[0].As<CallSyntax>().MemberName.Should().Be("method_call");
        }

        [TestMethod]
        public void Can_parse_code_block_with_multiple_statements()
        {
            var syntax = CodeBlockSyntax.Parse(@"method_call1
method_call2
method_call3
");

            syntax.Statements[0].As<CallSyntax>().MemberName.Should().Be("method_call1");
            syntax.Statements[1].As<CallSyntax>().MemberName.Should().Be("method_call2");
            syntax.Statements[2].As<CallSyntax>().MemberName.Should().Be("method_call3");
        }

        [TestMethod]
        public void Can_parse_statement_with_leading_whitespaces()
        {
            var syntax = CodeBlockSyntax.Parse(@"method_call1
    with_leading_spaces
    with_leading_tabs
");
            syntax.Statements[0].As<CallSyntax>().MemberName.Should().Be("method_call1");
            syntax.Statements[1].As<CallSyntax>().MemberName.Should().Be("with_leading_spaces");
            syntax.Statements[2].As<CallSyntax>().MemberName.Should().Be("with_leading_tabs");
        }

        [TestMethod]
        public void Can_parse_if()
        {
            var syntax = CodeBlockSyntax.Parse(@"if (1==1)
    func1
elseif (2==2)
else
    func2
endif
");
            syntax.Statements.Length.Should().Be(1);
            syntax.Statements[0].As<IfSyntax>().Should().NotBeNull();
        }

        [TestMethod]
        public void Can_parse_assignment()
        {
            var syntax = CodeBlockSyntax.Parse(@"var1=1");
            syntax.Statements[0].As<AssignmentSyntax>().Should().NotBeNull();
        }

        [TestMethod]
        public void Can_parse_return_statement_with_return_value()
        {
            var syntax = CodeBlockSyntax.Parse(@"return 1");
            var argument = syntax.Statements[0].Should().BeOfType<ReturnSyntax>().Which.Argument;
            argument.Should().BeOfType<TextArgumentSyntax>().Which.Text.Should().Be("1");
        }

        [TestMethod]
        public void Can_parse_complex_block()
        {
            var syntax = CodeBlockSyntax.Parse(@"call1
if (1==1)
    call2
endif
var1=1
");

            syntax.Statements[0].As<CallSyntax>().MemberName.Should().Be("call1");
            syntax.Statements[1].As<IfSyntax>().Should().NotBeNull();
            syntax.Statements[2].As<AssignmentSyntax>().Should().NotBeNull();
        }

        [TestMethod]
        public void Can_parse_empty_lines()
        {
            var syntax = CodeBlockSyntax.Parse(@"
call1

call2");

            syntax.Statements.Length.Should().Be(2);
            syntax.Statements[0].As<CallSyntax>().Should().NotBeNull();
            syntax.Statements[1].As<CallSyntax>().Should().NotBeNull();
        }

        [TestMethod]
        public void Can_parse_empty_lines_with_comments()
        {
            var syntax = CodeBlockSyntax.Parse(@"//comment
call1
//comment
call2");

            syntax.Statements.Length.Should().Be(2);
            syntax.Statements[0].As<CallSyntax>().Should().NotBeNull();
            syntax.Statements[1].As<CallSyntax>().Should().NotBeNull();
        }

        [TestMethod]
        public void Can_parse_statements_terminated_by_comment()
        {
            var syntax = CodeBlockSyntax.Parse(@"call1 //comment
call2 //comment");

            syntax.Statements.Should().HaveCount(2);
            syntax.Statements[0].Should().BeOfType<CallSyntax>();
            syntax.Statements[1].Should().BeOfType<CallSyntax>();
        }

        [TestMethod]
        public void Can_parse_two_consecutive_calls_with_arguments()
        {
            var syntax = CodeBlockSyntax.Parse(@"DIALOG D_RACEclass_background
DIALOG D_RACEclass_background");

            syntax.Statements.Should().HaveCount(2);
            syntax.Statements[0].Should().BeOfType<CallSyntax>();
            syntax.Statements[1].Should().BeOfType<CallSyntax>();
        }

        [TestMethod]
        public void Event_header_terminates_code_block()
        {
            var syntax = CodeBlockSyntax.Parse(@"call1

on=@userdclick");

            syntax.Statements.Should().HaveCount(1);
            syntax.Statements[0].Should().BeOfType<CallSyntax>().Which.MemberName.Should().Be("call1");
        }

        [TestMethod]
        public void Can_parse_macro_statement()
        {
            var codeBlockSyntax = CodeBlockSyntax.Parse(@"<argo.tag(back)>");

            codeBlockSyntax.Statements.Should().HaveCount(1);
            var statementSyntax = codeBlockSyntax.Statements[0].Should().BeOfType<MacroStatementSyntax>().Which;
        }

        [TestMethod]
        public void Can_parse_doswitch_followed_by_call()
        {
            var codeBlockSyntax = CodeBlockSyntax.Parse(@"doswitch argn
    <blank>
    tag(nation, Gondoran)
enddo
call1");

            codeBlockSyntax.Statements.Should().HaveCount(2);
            codeBlockSyntax.Statements[0].Should().BeOfType<DoSwitchSyntax>();
            codeBlockSyntax.Statements[1].Should().BeOfType<CallSyntax>();
        }
    }
}
