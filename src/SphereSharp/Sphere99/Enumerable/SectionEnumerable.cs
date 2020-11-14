using Antlr4.Runtime.Tree;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SphereSharp.Sphere99.Enumerable
{
    public class SectionEnumerable<T> : IEnumerable<SectionParsingResult<T>>
        where T : IParseTree
    {
        private readonly string content;
        private readonly Func<string, ParsingResult<T>> parser;

        public SectionEnumerable(string content, Func<string, ParsingResult<T>> parser)
        {
            this.content = content;
            this.parser = parser;
        }

        public IEnumerator<SectionParsingResult<T>> GetEnumerator() => new SectionEnumerator<T>(content, parser);
        IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
    }
}
