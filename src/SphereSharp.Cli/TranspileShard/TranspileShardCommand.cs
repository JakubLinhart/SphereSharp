using SphereSharp.Sphere99;
using SphereSharp.Sphere99.Sphere56Transpiler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Cli.TranspileShard
{
    public sealed class TranspileShardCommand
    {
        private readonly Compilation compilation = new Compilation();
        private string currentDirectory;
        private ShardSettings settings;
        private TranspileShardOptions options;

        public void Transpile(TranspileShardOptions options)
        {
            this.options = options;

            if (!File.Exists(options.SettingsFile))
            {
                Console.WriteLine($"Setting file {options.SettingsFile} doesn't exist.");
                return;
            }

            string settingsFileContent = File.ReadAllText(options.SettingsFile);
            currentDirectory = Path.GetDirectoryName(options.SettingsFile);

            settings = Newtonsoft.Json.JsonConvert.DeserializeObject<ShardSettings>(settingsFileContent);

            string scriptsPath = new DirectoryInfo(Path.Combine(currentDirectory, settings.ScriptsPath)).FullName;
            Console.WriteLine($"Parsing script directory {scriptsPath}");
            ParseScriptDirectory(scriptsPath);

            string savePath = new DirectoryInfo(Path.Combine(currentDirectory, settings.SavePath)).FullName;
            Console.WriteLine();
            Console.WriteLine($"Parsing save directory {savePath}");

            ParseCharSave(savePath);
            ParseWorldSave(savePath);

            if (compilation.CompilationErrors.Any())
            {
                Console.WriteLine();
                Console.WriteLine("Compilation errors found.");
                return;
            }

            TranspileScripts();
            TranspileCharSave();
            TranspileWorldSave();
        }

        private void TranspileWorldSave()
        {
            var outputWorldFileName = Path.Combine(options.OutputPath, "save", "sphereworld.scp");
            var outputDataFileName = Path.Combine(options.OutputPath, "save", "spheredata.scp");
            Console.WriteLine($"Transpiling to {outputWorldFileName}");
            Console.WriteLine($"Transpiling to {outputDataFileName}");
            var transpiler = new WorldTranspiler(compilation.DefinitionRepository);

            WorldTranspilationResult result = transpiler.Transpile(compilation.CompiledWorldSaveFile.ParsedTree);

            File.WriteAllText(outputWorldFileName, result.World);
            File.WriteAllText(outputDataFileName, result.Data);
        }

        private void TranspileCharSave()
        {
            var outputFileName = Path.Combine(options.OutputPath, "save", "spherechars.scp");
            Console.WriteLine($"Transpiling to {outputFileName}");
            var transpiler = new CharsTranspiler(compilation.DefinitionRepository);

            string result = transpiler.Transpile(compilation.CompiledCharSaveFile.ParsedTree);

            File.WriteAllText(outputFileName, result);
        }

        private string GetOutputFileName(string fileName)
        {
            var relativeFileName = GetRelativeInputFile(fileName);
            return Path.Combine(options.OutputPath, relativeFileName);
        }

        private void TranspileScripts()
        {
            foreach (var file in compilation.CompiledFiles)
            {
                var outputFileName = GetOutputFileName(file.FileName);
                var outputDirectory = Path.GetDirectoryName(outputFileName);
                if (!Directory.Exists(outputDirectory))
                    Directory.CreateDirectory(outputDirectory);

                Console.WriteLine($"Transpiling to {outputFileName}");
                var transpiler = new Sphere56TranspilerVisitor(compilation.DefinitionRepository);
                transpiler.Visit(file.ParsedTree);
                File.WriteAllText(outputFileName, transpiler.Output);
            }
        }

        private void ParseSaveFile(string savePath, string name, Action<string, string> action)
        {
            var fileName = new FileInfo(Path.Combine(savePath, name)).FullName;
            var relativeFileName = GetRelativeInputFile(fileName);

            Console.WriteLine($"Parsing {relativeFileName}");
            string src = File.ReadAllText(fileName);

            action(fileName, src);
        }

        private void ParseWorldSave(string savePath) 
            => ParseSaveFile(savePath, "sphereworld.scp", (name, src) => compilation.AddWorldSaveFile(name, src));
        private void ParseCharSave(string savePath) 
            => ParseSaveFile(savePath, "spherechars.scp", (name, src) => compilation.AddCharSaveFile(name, src));

        private string GetRelativeOutputFile(string fileName)
        {
            int baseDirectoryLength = options.OutputPath.Length;
            return FixPath(fileName.Substring(baseDirectoryLength, fileName.Length - baseDirectoryLength));
        }

        private string GetRelativeInputFile(string fileName)
        {
            int baseDirectoryLength = currentDirectory.Length;
            return FixPath(fileName.Substring(baseDirectoryLength, fileName.Length - baseDirectoryLength));
        }

        private string FixPath(string fileName) => fileName.Replace('\\', '/').Trim('/');
        private bool IsIgnored(string fileName)
        {
            string fixedFileName = FixPath(fileName);

            return settings.IgnoredScripts
                .Select(FixPath)
                .Any(x => x.Equals(fixedFileName, StringComparison.OrdinalIgnoreCase));
        }

        private void ParseFile(string fileName)
        {
            var relativeFileName = GetRelativeInputFile(fileName);

            if (IsIgnored(relativeFileName))
            {
                Console.WriteLine($"Ignoring file {relativeFileName}");
                return;
            }

            Console.WriteLine($"Parsing {relativeFileName}");
            string src = File.ReadAllText(fileName);

            compilation.AddFile(fileName, src);
        }

        private void ParseScriptDirectory(string inputDirectory)
        {
            foreach (var file in Directory.GetFiles(inputDirectory, "*.scp"))
            {
                ParseFile(new FileInfo(file).FullName);
            }

            foreach (var dir in Directory.GetDirectories(inputDirectory))
            {
                var dirName = Path.GetFileName(dir);
                string inputDir = Path.Combine(inputDirectory, dirName);
                ParseScriptDirectory(inputDir);
            }
        }
    }
}
