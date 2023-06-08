using System.IO;
using System.Text.Json;

namespace ASNC.Data
{
    public class Config
    {
        internal const string ConfigFilePath = "./config.json";

        public string? RetailKeyPath { get; set; }

        public bool DownloadMissingImagesOnLoad { get; set; } = true;

        public bool DownloadInfoDataOnStart { get; set; } = false;

        internal static bool Exists => File.Exists(ConfigFilePath);

        internal static Config Load()
        {
            if (File.Exists(ConfigFilePath))
            {
                var json = File.ReadAllText(ConfigFilePath);
                return JsonSerializer.Deserialize<Config>(json) ?? new Config();
            }

            return new Config();
        }

        internal void Save()
        {
            var json = JsonSerializer.Serialize(this, new JsonSerializerOptions() { WriteIndented = true });
            File.WriteAllText(ConfigFilePath, json);
        }
    }
}
