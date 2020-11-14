using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SphereSharp.Sphere99
{
    public class ParsingResult
    {
        public ParsingResult(IParseTree tree) : this(tree, System.Linq.Enumerable.Empty<Error>())
        {
        }

        public ParsingResult(IParseTree tree, IEnumerable<Error> errors)
        {
            Tree = tree;
            Errors = errors.ToArray();
        }

        public IParseTree Tree { get; }
        public Error[] Errors { get; }

        public string GetErrorsText(string separator = null)
        {
            separator = separator ?? Environment.NewLine;
            return Errors.Select(x => $"{x.Line},{x.Column}:{x.Message}").Aggregate((l, r) => l + separator + r);
        }
    }

    public sealed class ParsingResult<T> : ParsingResult
        where T : IParseTree
    {
        public ParsingResult(T tree, IEnumerable<Error> errors) : base(tree, errors)
        {
            this.Tree = tree;
        }

        public new T Tree { get; }
    }
}
