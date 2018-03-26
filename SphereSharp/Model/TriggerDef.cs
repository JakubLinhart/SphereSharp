using SphereSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace SphereSharp.Model
{
    public class TriggerDef
    {
        public string Name { get; }
        public CodeBlockSyntax CodeBlock { get; }

        public TriggerDef(string name, CodeBlockSyntax codeBlock)
        {
            Name = name;
            CodeBlock = codeBlock;
        }
    }
}
