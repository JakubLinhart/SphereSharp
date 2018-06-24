using CommandLine;

namespace SphereSharp.Cli.CreateShardSettings
{
    [Verb("create-shard-settings")]
    public sealed class CreateShardSettingsOptions
    {
        [Option('s', "settings-file", HelpText = "Shard settings file name.", Required = true)]
        public string SettingsFile { get; set; }
    }
}