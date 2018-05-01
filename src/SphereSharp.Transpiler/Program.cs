using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Transpiler
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("SphereSharp.Transpiler.exe <input file>");
                return;
            }

            string inputFileName = args[0];

            var inputStream = new AntlrInputStream(File.OpenRead(inputFileName));
            var lexer = new sphereScript99Lexer(inputStream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new sphereScript99Parser(tokenStream);
            parser.AddErrorListener(new ConsoleErrorListener());

            var file = parser.file();
            var generator = new Sphere99RoundtripGenerator();
            generator.Visit(file);

            Console.WriteLine(generator.Output);
        }
    }
}
