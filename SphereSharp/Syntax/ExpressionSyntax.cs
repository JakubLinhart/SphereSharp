using Sprache;
using System;
using System.Collections.Generic;
using System.Text;

namespace SphereSharp.Syntax
{
    public abstract class ExpressionSyntax : SyntaxNode
    {
        public bool Enclosed { get; private set; }

        public static ExpressionSyntax Parse(string src) =>
            ExpressionParser.Expr.Parse(src);

        internal ExpressionSyntax Enclose()
        {
            Enclosed = true;
            return this;
        }
    }
}
