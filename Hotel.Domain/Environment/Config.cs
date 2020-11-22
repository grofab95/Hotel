using Hotel.Domain.Utilities;
using System;
using System.IO;
using System.Text.Json;

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
            CreateDataFolderIfNotExist();
            var configPath = GetConfigFilePath();
            var configJson = File.ReadAllText($"{configPath}/config.json");

            ValidConfig(configJson);

            Config config = null;

            try
            {
                config = JsonUtility.ParseToObject<Config>(configJson);
            }
            catch (Exception ex)
            {
                throw new Exception($"błąd podczas odczytywania pliku config.json: {ex.Message}.");
            }

            return config;
        }

        private static string GetConfigFilePath()
        {
            var configFileName = "config.json";

            var configDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
            while (!File.Exists(Path.Combine(configDirectory.FullName, configFileName)))
            {
                configDirectory = new DirectoryInfo(configDirectory.FullName).Parent;
                if (configDirectory == null)
                    throw new Exception("Brak pliku config.json!");
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

        //public bool IsTestMode()
        //   => Environment.MachineName != "SERVER2019";
    }
}
