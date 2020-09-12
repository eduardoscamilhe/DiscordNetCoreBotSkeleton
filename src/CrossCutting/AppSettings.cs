using Newtonsoft.Json;
using System;
using System.IO;
namespace CrossCutting
{
    public class AppSettings
    {
        private static int creationCount = 0;

        private static readonly AppSettings _settings = new AppSettings();

        private AppSettings()
        {
            creationCount++;
        }

        public static AppSettings GetInstance()
        {
            _settings.StartConfiguration();
            _settings.BotToken = Environment.GetEnvironmentVariable("BotToken") ?? _settings.BotToken;
            _settings.Prefix = Environment.GetEnvironmentVariable("Prefix") ?? _settings.Prefix;
            return _settings;
        }

        public int GetCreationCount() => creationCount;
        public void StartConfiguration()
        {
            using (StreamReader r = new StreamReader("./appsettings.json"))
            {
                string json = r.ReadToEnd();
                var obj = JsonConvert.DeserializeObject<AppSettings>(json);
                this.BotToken = obj.BotToken;
                this.Prefix = obj.Prefix;
            }
        }

        public string BotToken { get; set; }
        public string Prefix { get; set; }

    }
}
