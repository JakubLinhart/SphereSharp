using Antlr4.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Parser
{
    public abstract class ParsingTestBase
    {
        protected void Parse(string src, Action<sphereScript99Parser> parserAction)
        {
            try
            {
                AntlrInputStream inputStream = new AntlrInputStream(src);
                var lexer = new sphereScript99Lexer(inputStream);
                var tokenStream = new CommonTokenStream(lexer);
                var parser = new sphereScript99Parser(tokenStream);
                var errorListener = new FailTestErrorListener();
                parser.AddErrorListener(errorListener);

                parserAction(parser);

                if (parser.InputStream.Index + 1 < parser.InputStream.Size)
                {
                    Assert.Fail($"Input stream not fully parsed index: {parser.InputStream.Index}, size: {parser.InputStream.Size}");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail($"Testing '{src}'\n\nMessage: {ex.Message}\n\n{ex}");
            }
        }
    }
}
