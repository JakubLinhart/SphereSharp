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
            var syntax = FileSyntax.Parse("asdf", File.ReadAllText(@"c:\Users\jakub\sources\ultima\erebor\sphere\scripts\newbie_portals.scp"));
            var generator = new Sphere99Generator();
            generator.Visit(syntax);

            Console.WriteLine(generator.ToString());
        }
    }
}
