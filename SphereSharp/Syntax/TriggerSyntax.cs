using Sprache;

namespace SphereSharp.Syntax
{
    public class TriggerSyntax
    {
        public string Name { get; }
        public CodeBlockSyntax CodeBlock { get; }

        public TriggerSyntax(string eventName, CodeBlockSyntax codeBlock)
        {
            Name = eventName;
            CodeBlock = codeBlock;
        }

        public static TriggerSyntax Parse(string src) => TriggerParser.Trigger.Parse(src);
    }
}
