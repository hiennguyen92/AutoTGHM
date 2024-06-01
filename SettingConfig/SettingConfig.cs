using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace SettingConfig
{
    public class SettingConfig
    {

        public static AppConfig LoadConfig()
        {
            try
            {
                string configFilePath = "appsettings.json";
                string jsonString = File.ReadAllText(configFilePath);
                AppConfig config = JsonSerializer.Deserialize<AppConfig>(jsonString);
                return config;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading configuration file: {ex.Message}");
                return null;
            }
        }

        public static void SaveConfig(AppConfig config)
        {
            try
            {
                string configFilePath = "appsettings.json";
                string jsonString = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(configFilePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving configuration file: {ex.Message}");
            }
        }

    }



    public class AppConfig
    {
        public string PathDir { get; set; }

    }
}
