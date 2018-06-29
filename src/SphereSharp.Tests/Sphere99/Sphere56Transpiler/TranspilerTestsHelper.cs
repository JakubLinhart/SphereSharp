using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SphereSharp.Sphere99;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Sphere99.Sphere56Transpiler
{
    public static class TranspilerTestsHelper
    {
        public static void TranspileStatementCheck(string input, string expectedOutput)
            => TranspileCheck(input, expectedOutput, (src, parser) => parser.ParseStatement(src));

        public static void TranspileFileCheck(string input, string expectedOutput)
            => TranspileCheck(input, expectedOutput, (src, parser) => parser.ParseFile(src));

        public static void TranspileSaveFileCheck(string input, string expectedOutput)
            => TranspileCheck(input, expectedOutput, (src, parser) => parser.ParseSaveFile(src));

        public static void TranspileConditionCheck(string input, string expectedOutput)
            => TranspileCheck(input, expectedOutput, (src, parser) => parser.ParseCondition(src));

        public static void TranspileCodeBlockCheck(string input, string expectedOutput)
            => TranspileCheck(input, expectedOutput, (src, parser) => parser.ParseCodeBlock(src));

        public static void TranspilePropertyAssignmentCheck(string input, string expectedOutput)
            => TranspileCheck(input, expectedOutput, (src, parser) => parser.ParsePropertyAssignment(src));

        public static void TranspileTriggerCheck(string input, string expectedOutput)
            => TranspileCheck(input, expectedOutput, (src, parser) => parser.ParseTrigger(src));

        private static void TranspileCheck(string input, string expectedOutput, Func<string, SphereSharp.Sphere99.Parser, ParsingResult> parseFunc)
        {
            var definitionsRepository = new DefinitionsRepository();
            var parser = new SphereSharp.Sphere99.Parser();
            var parsingOutput = parseFunc(input, parser);

            if (parsingOutput.Errors.Any())
            {
                Assert.Fail(parsingOutput.GetErrorsText());
            }

            new DefinitionsCollector(definitionsRepository).Visit(parsingOutput.Tree);

            var transpiler = new SphereSharp.Sphere99.Sphere56TranspilerVisitor(definitionsRepository);
            transpiler.Visit(parsingOutput.Tree);


            transpiler.Output.Trim().Should().Be(expectedOutput.Trim());
        }

    }
}
