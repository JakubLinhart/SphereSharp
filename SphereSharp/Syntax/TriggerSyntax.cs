using System.Collections.Generic;
using Sprache;

namespace SphereSharp.Syntax
{
    public class TriggerSyntax : SyntaxNode
    {
        public string Name { get; }
        public CodeBlockSyntax CodeBlock { get; }
        public bool IsNamedTrigger => !int.TryParse(Name, out int num);

        public TriggerSyntax(string eventName, CodeBlockSyntax codeBlock)
        {
            Name = eventName;
            CodeBlock = codeBlock;
        }

        public static TriggerSyntax Parse(string src) => TriggerParser.Trigger.Parse(src);

        public override void Accept(SyntaxVisitor visitor) => visitor.VisitTrigger(this);

        public override IEnumerable<SyntaxNode> GetChildNodes()
        {
            yield return CodeBlock;
        }
    }
}
