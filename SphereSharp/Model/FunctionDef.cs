using SphereSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Model
{
    public class FunctionDef
    {
        public string Name { get; }
        public CodeBlockSyntax Body { get; }

        public FunctionDef(string name, CodeBlockSyntax body)
        {
            Name = name;
            Body = body;
        }
    }
}
