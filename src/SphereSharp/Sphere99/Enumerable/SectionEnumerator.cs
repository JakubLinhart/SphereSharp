using Antlr4.Runtime.Tree;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SphereSharp.Sphere99.Enumerable
{
    public class SectionEnumerator<T> : IEnumerator<SectionParsingResult<T>>
        where T : IParseTree
    {
        private readonly string saveFileContent;
        private readonly Func<string, ParsingResult<T>> parser;

        private ParsingResult<sphereScript99Parser.SaveFileSectionContext> current;
        private int currentIndex = 0;
        private int sectionStart = -1;
        private int sectionStartLineOffset = -1;
        private int currentLineOffset = 0;

        public SectionEnumerator(string saveFileContnet, Func<string, ParsingResult<T>> parser)
        {
            this.saveFileContent = saveFileContnet;
            this.parser = parser;
        }

        public SectionParsingResult<T> Current => GetCurrentSection();

        object IEnumerator.Current => GetCurrentSection();

        private SectionParsingResult<T> GetCurrentSection()
        {
            if (sectionStart < 0)
                return null;

            var section = saveFileContent.Substring(sectionStart, currentIndex - sectionStart);
            return new SectionParsingResult<T>(parser(section), currentLineOffset);
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            sectionStart = -1;
            if (!SeekSectionStart()) return false;

            sectionStart = currentIndex;
            sectionStartLineOffset = currentLineOffset;
            currentIndex++;

            SeekSectionStart();
            return true;
        }

        private bool SeekSectionStart()
        {
            bool firstNonWhitespaceChar = true;
            while (currentIndex < saveFileContent.Length && (!firstNonWhitespaceChar || saveFileContent[currentIndex] != '['))
            {
                if (saveFileContent[currentIndex] == '\n')
                {
                    firstNonWhitespaceChar = true;
                    currentLineOffset++;
                }
                else if (firstNonWhitespaceChar && !char.IsWhiteSpace(saveFileContent[currentIndex]))
                    firstNonWhitespaceChar = false;
                currentIndex++;
            }

            return currentIndex < saveFileContent.Length;
        }

        public void Reset()
        {
            sectionStart = -1;
            currentIndex = 0;
        }
    }
}
