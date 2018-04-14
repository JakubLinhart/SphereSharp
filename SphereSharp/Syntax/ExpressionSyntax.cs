﻿using Sprache;
using System;
using System.Collections.Generic;
using System.Text;

namespace SphereSharp.Syntax
{
    public abstract class ExpressionSyntax : SyntaxNode
    {
        public static ExpressionSyntax Parse(string src) =>
            ExpressionParser.Expr.Parse(src);
    }
}
