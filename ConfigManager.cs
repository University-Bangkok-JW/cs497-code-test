using System;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class ConfigManager
{
    public string DbType { get; private set; }

    public ConfigManager()
    {
        EnsureConfigFileExists();
        LoadConfig();
    }

    private void EnsureConfigFileExists()
    {
        string configPath = "config.yml";
        if (!File.Exists(configPath))
        {
            Console.WriteLine("Creating default config.yml...");
            File.WriteAllText(configPath, "db: json");
        }
    }

    private void LoadConfig()
    {
        try
        {
            var yaml = File.ReadAllText("config.yml");
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .Build();

            var config = deserializer.Deserialize<ConfigData>(yaml);
            DbType = config.Db ?? "json"; // Default to JSON if missing
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading config: {ex.Message}");
            DbType = "json"; // Default if an error occurs
        }
    }

    private class ConfigData
    {
        public string Db { get; set; }
    }
}
