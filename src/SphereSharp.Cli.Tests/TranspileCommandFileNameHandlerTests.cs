using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SphereSharp.Cli.Transpile;

namespace SphereSharp.Cli.Tests
{
    [TestClass]
    public class TranspileCommandFileNameHandlerTests
    {
        [TestMethod]
        public void Can_map_input_file_within_InputPath_to_OutputPath()
        {
            var handler = new TranspileCommandFileNameHandler(new TranspileOptions()
            {
                InputPath = @"c:\input\",
                OutputPath = @"c:\path\to\output\",
            });

            string outputPath = handler.GetOututFileNameFromInput(@"c:\input\file1.scp");
            outputPath.Should().Be(@"c:\path\to\output\file1.scp");
        }

        [TestMethod]
        public void Can_map_input_file_within_InputPath_to_OutputPath_and_appends_suffix()
        {
            var handler = new TranspileCommandFileNameHandler(new TranspileOptions()
            {
                InputPath = @"c:\input\",
                OutputPath = @"c:\path\to\output\",
                OutputSuffix = ".56"
            });

            string outputPath = handler.GetOututFileNameFromInput(@"c:\input\file1.scp");
            outputPath.Should().Be(@"c:\path\to\output\file1.scp.56");
        }

        [TestMethod]
        public void When_no_output_path_specified_Then_appends_suffix_to_input_file_name_within_InputPath()
        {
            var handler = new TranspileCommandFileNameHandler(new TranspileOptions()
            {
                InputPath = @"c:\input\file1.scp",
                OutputSuffix = @".56",
            });

            string outputPath = handler.GetOututFileNameFromInput(@"c:\input\file1.scp");
            outputPath.Should().Be(@"c:\input\file1.scp.56");
        }


        [TestMethod]
        public void Can_add_dot_to_suffix_When_dot_not_specified()
        {
            var handler = new TranspileCommandFileNameHandler(new TranspileOptions()
            {
                InputPath = @"c:\input\file1.scp",
                OutputSuffix = @"56",
            });

            string outputPath = handler.GetOututFileNameFromInput(@"c:\input\file1.scp");
            outputPath.Should().Be(@"c:\input\file1.scp.56");
        }
    }
}
