using System;
using System.Collections.Generic;
using System.Text;

namespace SphereSharp.Fast.Save
{
    public struct Section
    {
        private readonly int start;
        private readonly int length;
        private readonly string content;

        public Section(int start, int length, string content)
        {
            this.start = start;
            this.length = length;
            this.content = content;
        }

        public SectionType Type
        {
            get
            {
                var sectionTypeSpan = GetSectionTypeSpan(content.AsSpan(start, length));
                if (sectionTypeSpan.Equals("VarNames", StringComparison.OrdinalIgnoreCase))
                    return SectionType.Vars;
                if (sectionTypeSpan.Equals("GMPage", StringComparison.OrdinalIgnoreCase))
                    return SectionType.GMPage;
                var worldSectionTypeSpan = sectionTypeSpan.Slice(5);
                if (worldSectionTypeSpan.Equals("Char", StringComparison.OrdinalIgnoreCase))
                    return SectionType.Char;
                if (worldSectionTypeSpan.Equals("Item", StringComparison.OrdinalIgnoreCase))
                    return SectionType.Item;

                throw new InvalidOperationException($"Unknown section type {sectionTypeSpan.ToString()}");
            }
        }

        public ReadOnlySpan<char> Name => GetSectionNameSpan(content.AsSpan(start, length));

        private ReadOnlySpan<char> GetSectionNameSpan(ReadOnlySpan<char> sectionSpan)
        {
            int index = 1;
            while (true)
            {
                if (index >= sectionSpan.Length)
                    throw new InvalidOperationException("Unexpected end of file.");
                if (sectionSpan[index] == ' ')
                    break;
                if (sectionSpan[index] == '\n')
                    throw new InvalidOperationException("Unexpected end of line.");
                index++;
            }

            index++;
            var startIndex = index;

            while (true)
            {
                if (index >= sectionSpan.Length)
                    throw new InvalidOperationException("Unexpected end of file.");
                if (sectionSpan[index] == ']')
                    break;
                if (sectionSpan[index] == '\n')
                    throw new InvalidOperationException("Unexpected end of line.");
                index++;
            }

            return sectionSpan.Slice(startIndex, index - startIndex);
        }

        private ReadOnlySpan<char> GetSectionTypeSpan(ReadOnlySpan<char> sectionSpan)
        {
            int index = 1;
            while (index < sectionSpan.Length)
            {
                if (sectionSpan[index] == ' ')
                    return sectionSpan.Slice(1, index - 1);
                if (sectionSpan[index] == '\n')
                    throw new InvalidOperationException("Unexpected end of line.");
                index++;
            }

            throw new InvalidOperationException("Unexpected end of line.");
        }
    }
}
