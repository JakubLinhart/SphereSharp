using SphereSharp.Fast.Save;
using System;
using System.Collections;

namespace SphereSharp.Sphere99.Enumerable
{
    public class SectionEnumerable
    {
        private readonly string content;

        public SectionEnumerable(string content)
        {
            this.content = content;
        }

        public SectionEnumerator GetEnumerator() => new SectionEnumerator(content);

        public ref struct SectionEnumerator
        {
            private ReadOnlySpan<char> currentSpan;
            private ReadOnlySpan<char> fileSpan;
            private int currentStart;
            private string content;

            public SectionEnumerator(string saveFileContent)
            {
                fileSpan = saveFileContent.AsSpan();
                currentStart = 0;
                content = saveFileContent;
                currentSpan = ReadOnlySpan<char>.Empty;
            }

            public Section Current => new Section(currentStart, currentSpan.Length, content);

            public bool MoveNext()
            {
                var seekLength = SeekSectionStart();
                if (fileSpan.Length == 0)
                    return false;

                currentStart += currentSpan.Length + seekLength;

                currentSpan = fileSpan;
                fileSpan = fileSpan.Slice(1);
                seekLength = SeekSectionStart();
                currentSpan = currentSpan.Slice(0, seekLength + 1);

                return true;
            }

            private int SeekSectionStart()
            {
                bool firstNonWhitespaceChar = true;
                int seekLength = 0;

                while (fileSpan.Length > 0 && (!firstNonWhitespaceChar || fileSpan[0] != '['))
                {
                    if (fileSpan[0] == '\n')
                        firstNonWhitespaceChar = true;
                    else if (firstNonWhitespaceChar && !fileSpan.IsWhiteSpace())
                        firstNonWhitespaceChar = false;
                    fileSpan = fileSpan.Slice(1);
                    seekLength++;
                }

                return seekLength;
            }

            public void Reset()
            {
            }
        }
    }

}
