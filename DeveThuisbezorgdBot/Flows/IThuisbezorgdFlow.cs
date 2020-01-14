using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace DeveThuisbezorgdBot.Flows
{
    public interface IThuisbezorgdFlow
    {
        Task Init(ChatState state, TelegramBotClient bot, Message message);

        Task<bool> ProcessMessage(ChatState state, TelegramBotClient bot, Message message);

        Task Finish(ChatState state, TelegramBotClient bot, Message message);
    }
}
