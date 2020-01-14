using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace DeveThuisbezorgdBot.Flows.Joiners
{
    public class RestaurantSelectionFlow : IThuisbezorgdFlow
    {
        private int[] _votes;

        public async Task Init(ChatState state, TelegramBotClient bot, Message message)
        {
            int restaurantCount = 5;

            var nl = Environment.NewLine;
            await bot.SendTextMessageAsync(state.ChatId, $"Choose one of the restaurants:{nl}1. Fatman{nl}2. Spareribs Leidscherein");

            _votes = new int[restaurantCount];
        }

        public async Task<bool> ProcessMessage(ChatState state, TelegramBotClient bot, Message message)
        {
            var couldParseAsNumber = int.TryParse(message.Text, out int number);
            if (couldParseAsNumber && number >= 0 && number < _votes.Length)
            {
                _votes[number]++;
            }
            else if (message.Text.Equals("!go", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public async Task Finish(ChatState state, TelegramBotClient bot, Message message)
        {
            var nl = Environment.NewLine;
            await bot.SendTextMessageAsync(state.ChatId, $"Votes:{nl}{string.Join(nl, _votes.Select((val, i) => $"{i}: {val}"))}");
        }
    }
}
