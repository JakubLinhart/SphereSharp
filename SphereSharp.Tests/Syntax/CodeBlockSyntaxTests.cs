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
    func2
else
    func3
endif
");
            syntax.Statements.Length.Should().Be(1);
            syntax.Statements[0].Should().BeOfType<IfSyntax>();
        }

        [TestMethod]
        public void Can_parse_while()
        {
            var syntax = CodeBlockSyntax.Parse(@"while (1==1)
    call1
endwhile
");
            syntax.Statements.Length.Should().Be(1);
            syntax.Statements[0].Should().BeOfType<WhileStatementSyntax>();
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
            argument.Should().BeOfType<ExpressionArgumentSyntax>();
        }

        [TestMethod]
        public void Can_parse_events_statment()
        {
            var syntax = CodeBlockSyntax.Parse(@"events +e_something");
            syntax.Statements.Should().HaveCount(1);

            syntax.Statements[0].Should().BeOfType<CallSyntax>().Which
                .MemberName.Should().Be("events");
        }

        [TestMethod]
        public void Can_parse_complex_block()
        {
            var syntax = CodeBlockSyntax.Parse(@"call1
if (1==1)
    call2
endif
var1=1
events +e_something

while (1==1)
    call1
endwhile
");

            syntax.Statements[0].Should().BeOfType<CallSyntax>().Which.MemberName.Should().Be("call1");
            syntax.Statements[1].Should().BeOfType<IfSyntax>();
            syntax.Statements[2].Should().BeOfType<AssignmentSyntax>();
            syntax.Statements[3].Should().BeOfType<CallSyntax>();
            syntax.Statements[4].Should().BeOfType<WhileStatementSyntax>();
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

        [TestMethod]
        public void Fails_when_parsing_incorect_statement()
        {
            var testedAction = (Action)(() => CodeBlockSyntax.Parse(@"call1 @#$ some bulshit"));
            testedAction.Should().Throw<Exception>();
        }

        [TestMethod]
        public void Can_parse_statement_with_whitespace_at_the_end_of_line()
        {
            var syntax = CodeBlockSyntax.Parse("call(asdf) ");

            syntax.Statements.Should().HaveCount(1);
        }

        [TestMethod]
        public void Can_parse_statement_with_comment_at_the_end_of_line()
        {
            var syntax = CodeBlockSyntax.Parse("call(asdf)//some comment");

            syntax.Statements.Should().HaveCount(1);
        }

        [TestMethod]
        public void Can_parse_statement_with_whitespace_and_comment_at_the_end_of_line()
        {
            var syntax = CodeBlockSyntax.Parse("call(asdf) //some comment");

            syntax.Statements.Should().HaveCount(1);
        }

        [TestMethod]
        public void Can_parse_two_assignments()
        {
            var syntax = CodeBlockSyntax.Parse(@"NPC=brain_undead
FAME=20");

            syntax.Statements.Should().HaveCount(2);
            syntax.Statements[0].Should().BeOfType<AssignmentSyntax>();
            syntax.Statements[1].Should().BeOfType<AssignmentSyntax>();
        }
    }
}
