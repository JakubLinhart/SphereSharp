using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Syntax
{
    public class FunctionSectionSyntax : SectionSyntax
    {
        public CodeBlockSyntax Body { get; }

        public FunctionSectionSyntax(string type, string name, string subType, CodeBlockSyntax body) : base(type, name, subType)
        {
            Body = body;
        }
    }
}
