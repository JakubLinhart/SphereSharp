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
    public class WhileStatementSyntaxTests
    {
        [TestMethod]
        public void Can_parse_while()
        {
            var syntax = WhileStatementSyntax.Parse(@"while (1==1)
    call1
endwhile
");
            syntax.Condition.Should().BeOfType<BinaryOperatorSyntax>();
            syntax.Body.Statements.Should().HaveCount(1);
        }
    }
}
