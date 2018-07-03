using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Cli.AnalyzeShard
{
    public sealed class AnalyzeShardCommand
    {
        private readonly Compilation compilation = new Compilation();
        private string currentDirectory;
        private ShardSettings settings;
        private AnalyzeShardOptions options;

        public void Analyze(AnalyzeShardOptions options)
        {
            this.options = options;

            if (!File.Exists(options.SettingsFile))
            {
                Console.WriteLine($"Setting file {options.SettingsFile} doesn't exist, creating empty settings file.");
                settings = new ShardSettings();
                File.WriteAllText(options.SettingsFile, JsonConvert.SerializeObject(settings, Formatting.Indented));
                return;
            }

            Console.WriteLine($"Reading setting file {new FileInfo(options.SettingsFile).FullName}");
            string settingsFileContent = File.ReadAllText(options.SettingsFile);
            currentDirectory = new DirectoryInfo(Path.GetDirectoryName(options.SettingsFile)).FullName;
            Console.WriteLine($"Shard directory: {currentDirectory}");

            Console.WriteLine();

            settings = JsonConvert.DeserializeObject<ShardSettings>(settingsFileContent);

            string scriptsPath = new DirectoryInfo(Path.Combine(currentDirectory, settings.ScriptsPath)).FullName;
            Console.WriteLine($"Parsing script directory {scriptsPath}");
            ParseScriptDirectory(scriptsPath);
        }

        private string FixPath(string fileName) => fileName.Replace('\\', '/').Trim('/');
        private bool IsIgnored(string fileName)
        {
            string fixedFileName = FixPath(fileName);

            return settings.IgnoredScripts
                .Select(FixPath)
                .Any(x => x.Equals(fixedFileName, StringComparison.OrdinalIgnoreCase));
        }

        private string GetRelativeInputFile(string fileName)
        {
            int baseDirectoryLength = currentDirectory.Length;
            return FixPath(fileName.Substring(baseDirectoryLength, fileName.Length - baseDirectoryLength));
        }

        private void ParseFile(string fileName)
        {
            var relativeFileName = GetRelativeInputFile(fileName);

            if (IsIgnored(relativeFileName))
            {
                Console.WriteLine($"Ignoring file {relativeFileName}");
                return;
            }

            string src = File.ReadAllText(fileName);

            Console.WriteLine($"Parsing {relativeFileName}");
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
