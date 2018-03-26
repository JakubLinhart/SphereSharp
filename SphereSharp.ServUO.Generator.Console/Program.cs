using SphereSharp.Model;
using SphereSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.ServUO.Generator
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("ConsoleGenerator.exe <path to sphere script directory> <path to output directory>");
                return -1;
            }

            if (!Directory.Exists(args[0]))
            {
                Console.WriteLine($"Path {args[0]} doesn't exist");
                return -1;
            }

            string outputDir = args[1];
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            var builder = new CodeModelBuilder();
            builder.LoadDirectory(args[0], null, Console.Out);

            var model = builder.Build();

            var generator = new Generator("UOErebor", model);

            foreach (var itemDef in model.ItemDefs)
            {
                var csharp = generator.Generate(itemDef);
                File.WriteAllText(Path.Combine(outputDir, Path.ChangeExtension(itemDef.DefName, "cs")), csharp);
            }

            foreach (var charDef in model.CharDefs)
            {
                var csharp = generator.Generate(charDef);
                File.WriteAllText(Path.Combine(outputDir, Path.ChangeExtension(charDef.DefName, "cs")), csharp);
            }

            foreach (var gumpDef in model.GumpDefs)
            {
                var csharp = generator.Generate(gumpDef);
                File.WriteAllText(Path.Combine(outputDir, Path.ChangeExtension(gumpDef.DefName, "cs")), csharp);
            }

            return 0;
        }
    }
}
