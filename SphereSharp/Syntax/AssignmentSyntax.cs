using System;
using System.Collections.Generic;
using System.Text;
using Sprache;

namespace SphereSharp.Syntax
{
    public sealed class AssignmentSyntax : StatementSyntax
    {
        public CallSyntax LValue { get; }
        public ExpressionSyntax RValue { get; }

        public AssignmentSyntax(CallSyntax lValue, ExpressionSyntax rValue)
        {
            RValue = rValue;
            LValue = lValue;
        }

        public static AssignmentSyntax Parse(string src) 
            => AssignmentParser.Assignment.Parse(src);
    }
}
