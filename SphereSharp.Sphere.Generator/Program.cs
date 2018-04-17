using SphereSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Sphere.Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("<input file> <output file>");
                return;
            }

            var syntax = FileSyntax.Parse("asdf", File.ReadAllText(args[0]));
            var generator = new Sphere99Generator();
            generator.Visit(syntax);

            string generatedSourceCode = generator.ToString();
            Console.WriteLine(generatedSourceCode);
            File.WriteAllText(args[1], generatedSourceCode);
        }
    }
}
