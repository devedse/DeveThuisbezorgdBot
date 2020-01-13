using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DeveThuisbezorgdBot.Config
{
    public static class BotConfigLoader
    {
        private const string AlternativeConfigFileLocation = @"C:\XGitPrivate\DeveThuisbezorgdBotConfig.json";

        public static BotConfig LoadFromStaticFile()
        {
            if (System.IO.File.Exists(AlternativeConfigFileLocation))
            {
                var txtConfig = System.IO.File.ReadAllText(AlternativeConfigFileLocation);
                var myteParkingExpenserConfig = JsonConvert.DeserializeObject<BotConfig>(txtConfig);
                return myteParkingExpenserConfig;
            }
            else
            {
                return null;
            }
        }

        public static BotConfig LoadFromEnvironmentVariables(IConfiguration configuration)
        {
            var config = new BotConfig()
            {
                TelegramBotToken = configuration.GetValue<string>("TelegramBotToken")
            };

            return config;
        }
    }
}
