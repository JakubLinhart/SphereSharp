using System;
using System.Collections.Generic;
using System.Text;
using Sprache;

namespace SphereSharp.Syntax
{
    public sealed class AssignmentSyntax : StatementSyntax
    {
        public CallSyntax LValue { get; }
        public ArgumentSyntax RValue { get; }

        public AssignmentSyntax(CallSyntax lValue, ArgumentSyntax rValue)
        {
            RValue = rValue;
            LValue = lValue;
        }

        public static AssignmentSyntax Parse(string src) 
            => AssignmentParser.Assignment.Parse(src);

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitAssignment(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield return LValue;
            yield return RValue;
        }
    }
}
