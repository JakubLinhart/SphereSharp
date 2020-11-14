using Antlr4.Runtime.Tree;

namespace SphereSharp.Sphere99.Enumerable
{
    public class SectionParsingResult<T>
        where T : IParseTree
    {
        public SectionParsingResult(ParsingResult<T> result, int lineOffset)
        {
            this.result = result;
            LineOffset = lineOffset;
        }

        private readonly ParsingResult<T> result;
        public int LineOffset { get; }

        public T Tree => result.Tree;
        public Error[] Errors => result.Errors;

        public string GetErrorsText(string separator = null) => result.GetErrorsText(separator);
    }
}
