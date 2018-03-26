using System;
using System.Collections.Generic;
using System.Text;

namespace SphereSharp.Syntax
{
    public class MacroExpressionSyntax : ExpressionSyntax
    {
        public MacroSyntax Macro { get; }

        public MacroExpressionSyntax(MacroSyntax macro)
        {
            Macro = macro;
        }
    }
}
