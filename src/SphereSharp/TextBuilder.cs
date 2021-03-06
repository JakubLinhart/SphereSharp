﻿using System;
using System.Linq;
using System.Text;
using Antlr4.Runtime.Tree;

namespace SphereSharp
{
    public class TextBuilder
    {
        private StringBuilder output = new StringBuilder();
        public string Output => output.ToString();

        public int Length => output.Length;

        public void AppendSubstring(int startIndex, int endIndex)
        {
            for (int i = startIndex; i <= endIndex; i++)
                output.Append(output[i]);
        }

        public void Append(ITerminalNode node) 
            => output.Append(Format(node));
        public void Append(ITerminalNode[] nodes)
            => output.Append(Format(nodes));

        public void Append(string str) => output.Append(str);
        public void Append(char ch) => output.Append(ch);

        public void AppendLine(string str) => output.AppendLine(str);

        public void AppendLine(char ch)
        {
            output.Append(ch);
            output.AppendLine();
        }

        public void AppendLine() => output.AppendLine();
        public void AppendLine(ITerminalNode[] nodes)
            => output.AppendLine(nodes?.Select(x => x.GetText()).Aggregate(string.Empty, (l, r) => l + r) ?? string.Empty);

        private string Format(ITerminalNode node) => node?.GetText() ?? string.Empty;
        private string Format(ITerminalNode[] nodes) => nodes?.Select(x => x.GetText()).Aggregate(string.Empty, (l, r) => l + r) ?? string.Empty;

        internal void EnsureNewline()
        {
            if (output[output.Length - 1] != '\n')
                output.AppendLine();
        }
    }
}
