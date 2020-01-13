using DeveCoolLib.Logging;
using DeveThuisbezorgdBot.PocoObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace DeveThuisbezorgdBot
{
    public class ChatState
    {
        public ThuisbezorgdState CurrentState { get; set; }
        public IEnumerator<string> EnumeratorThing { get; set; }
        public long ChatId { get; }

        public Dictionary<long, int> Points = new Dictionary<long, int>();
        private readonly ILogger _logger;
        private readonly GlobalBotState _globalBotState;

        public ChatState(ILogger logger, GlobalBotState globalBotState, long chatId)
        {
            _logger = logger;
            _globalBotState = globalBotState;
            ChatId = chatId;
        }

        public async Task HandleMessage(TelegramBotClient bot, Message message)
        {
            var msg = message.Text;

            if (msg.Equals("!help"))
            {
                await DisplayHelp(bot);
            }
            else if (msg.Equals("!food"))
            {
                CurrentState = new ThuisbezorgdState()
                {
                    Initiator = message.From.Id
                };
            }

            if (CurrentState != null)
            {

            }

        }


        public async Task DisplayHelp(TelegramBotClient bot)
        {
            var sb = new StringBuilder();
            sb.AppendLine("This is the thuisbezorgd bot");

            await bot.SendTextMessageAsync(ChatId, sb.ToString());
        }

        public string GetName(long id)
        {
            var user = _globalBotState.AllUsers[id];
            if (!string.IsNullOrWhiteSpace(user.FirstName) && !string.IsNullOrWhiteSpace(user.LastName))
            {
                return $"{user.FirstName} {user.LastName}";
            }
            else if (!string.IsNullOrWhiteSpace(user.FirstName))
            {
                return user.FirstName;
            }
            else if (!string.IsNullOrWhiteSpace(user.Username))
            {
                return user.Username;
            }
            return user.Id.ToString();
        }
    }
}
