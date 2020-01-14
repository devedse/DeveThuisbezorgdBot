using DeveThuisbezorgdBot.Config;
using System;
using System.Threading.Tasks;

namespace DeveThuisbezorgdBot.TelegramBot
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var config = BotConfigLoader.LoadFromStaticFile();

            var bot = new DeveThuisbezorgdTelegramBot(config);
            await bot.Start();
        }
    }
}
