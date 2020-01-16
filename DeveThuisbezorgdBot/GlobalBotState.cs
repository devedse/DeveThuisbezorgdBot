using System.Collections.Concurrent;
using Telegram.Bot.Types;

namespace DeveThuisbezorgdBot
{
    public class GlobalBotState
    {
        public ConcurrentDictionary<long, User> AllUsers { get; set; } = new ConcurrentDictionary<long, User>();
    }
}
