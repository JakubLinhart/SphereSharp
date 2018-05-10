using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace SphereSharp.Sphere99
{
    public class RoundtripGenerator : sphereScript99BaseVisitor<bool>
    {
        public SourceCodeBuilder output = new SourceCodeBuilder();
        public string Output => output.Output;

        public override bool VisitTerminal(ITerminalNode node)
        {
            if (!node.GetText().Contains("EOF"))
                output.Append(node);

            return base.VisitTerminal(node);
        }
    }
}
