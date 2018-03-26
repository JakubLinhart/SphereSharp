using System;
using System.Collections.Generic;
using System.Text;

namespace SphereSharp.Syntax
{
    public class DialogSectionSyntax : SectionSyntax
    {
        public CodeBlockSyntax InitCodeBlock { get; }

        public DialogSectionSyntax(string type, string name, string subName, CodeBlockSyntax initCodeBlock)
            : base(type, name, subName)
        {
            InitCodeBlock = initCodeBlock;
        }
    }
}
