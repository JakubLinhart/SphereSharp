using System.Linq;
using System.Text;
using Antlr4.Runtime.Tree;

namespace SphereSharp
{
    public class SourceCodeBuilder
    {
        private StringBuilder output = new StringBuilder();
        public string Output => output.ToString();

        public void Append(ITerminalNode node) 
            => output.Append(node?.GetText() ?? string.Empty);

        public void Append(ITerminalNode[] nodes)
            => output.Append(nodes?.Select(x => x.GetText()).Aggregate(string.Empty, (l, r) => l + r) ?? string.Empty);

        public void Append(string str) => output.Append(str);
        public void Append(char ch) => output.Append(ch);
    }
}
