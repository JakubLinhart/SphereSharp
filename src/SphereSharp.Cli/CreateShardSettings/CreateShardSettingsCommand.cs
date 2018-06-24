using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Cli.CreateShardSettings
{
    public sealed class CreateShardSettingsCommand
    {
        public void Create(CreateShardSettingsOptions options)
        {
            if (!File.Exists(options.SettingsFile))
            {
                Console.WriteLine($"Creating settings file {options.SettingsFile}.");
                var settings = new ShardSettings();
                settings.IgnoredScripts = new string[]
                {
                    @"scripts/just/example/some/file.scp"
                };
                settings.SavePath = "./save";
                settings.ScriptsPath = "./scripts";
                settings.PrePatches = new[]
                {
                    new PatchSet()
                    {
                        Scope = "scripts/just/example/some.file.scp",
                        Patches = new Dictionary<string, string>()
                        {
                            { "\"dialog(D_RACEclass_classes)\"", "\"dialog D_RACEclass_classes\"" },
                        }
                    }
                };
                File.WriteAllText(options.SettingsFile, JsonConvert.SerializeObject(settings, Formatting.Indented));
            }
            else
            {
                Console.WriteLine($"Settings file {options.SettingsFile} already exists.");
            }
        }
    }
}
