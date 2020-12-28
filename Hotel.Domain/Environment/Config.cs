using Hotel.Domain.Utilities;
using System;
using System.IO;

namespace Hotel.Domain.Environment
{
    public class Config
    {
        public string SqlConnection { get; set; }
        public string WebAddress { get; set; }
        public string WebPort { get; set; }
        public decimal PriceForStay { get; set; }
        public int FreeRoomHour { get; set; }

        public static Config Get => GetConfig();

        private static Config GetConfig()
        {
            try
            {
                CreateDataFolderIfNotExist();
                var configPath = GetConfigFilePath();
                var configJson = File.ReadAllText($"{configPath}/config.json");

                ValidConfig(configJson);
           
                return JsonUtility.ParseToObject<Config>(configJson);
            }
            catch (Exception ex)
            {
                throw new Exception($"błąd podczas odczytywania pliku config.json: {ex.Message}.");
            }
        }

        private static string GetConfigFilePath()
        {
            var configFileName = "config.json";

            var configDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
            while (!File.Exists(Path.Combine(configDirectory.FullName, configFileName)))
            {
                configDirectory = new DirectoryInfo(configDirectory.FullName)?.Parent
                    ?? throw new Exception("Brak pliku config.json!");
            }

            return configDirectory.FullName;
        }

        private static void CreateDataFolderIfNotExist()
        {
            if (!Directory.Exists("data"))
                Directory.CreateDirectory("data");

            var currentDir = Directory.GetCurrentDirectory();
            if (currentDir.Contains("Hotel.Web"))
            {
                if (!Directory.Exists("wwwroot/docs"))
                    Directory.CreateDirectory("wwwroot/docs");
            }
        }

        private static void ValidConfig(string configJson)
        {
            try
            {
                JsonUtility.ParseToObject<Config>(configJson);
            }
            catch (Exception ex)
            {
                throw new Exception("Nieprawidłowy config.");
            }
        }
    }
}
