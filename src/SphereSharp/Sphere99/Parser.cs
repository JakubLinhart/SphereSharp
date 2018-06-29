using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Sphere99
{
    public sealed class Parser
    {
        private ParsingResult<T> Parse<T>(string src, Func<sphereScript99Parser, T> parseAction)
            where T : IParseTree
        {
            var inputStream = new AntlrInputStream(src);
            var lexer = new sphereScript99Lexer(inputStream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new sphereScript99Parser(tokenStream);
            var errorListener = new MemoryErrorListener();

            parser.AddErrorListener(errorListener);

            var result = parseAction(parser);

            return new ParsingResult<T>(result, errorListener.Errors);

        }

        public ParsingResult<sphereScript99Parser.FileContext> ParseFile(string src)
            => Parse(src, (parser) => parser.file());

        public ParsingResult<sphereScript99Parser.SaveFileContext> ParseSaveFile(string src)
            => Parse(src, (parser) => parser.saveFile());

        public ParsingResult<sphereScript99Parser.StatementContext> ParseStatement(string src) 
            => Parse(src, parser => parser.statement());

        public ParsingResult<sphereScript99Parser.SectionContext> ParseSection(string src)
            => Parse(src, parser => parser.section());

        public ParsingResult<sphereScript99Parser.ConditionContext> ParseCondition(string src)
            => Parse(src, parser => parser.condition());

        public ParsingResult<sphereScript99Parser.CodeBlockContext> ParseCodeBlock(string src)
            => Parse(src, parser => parser.codeBlock());

        public ParsingResult<sphereScript99Parser.PropertyAssignmentContext> ParsePropertyAssignment(string src)
            => Parse(src, parser => parser.propertyAssignment());

        public ParsingResult<sphereScript99Parser.TriggerContext> ParseTrigger(string src)
            => Parse(src, parser => parser.trigger());
    }
}
