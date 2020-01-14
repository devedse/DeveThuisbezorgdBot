using DeveThuisbezorgdBot.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace DeveThuisbezorgdBot.Flows.Joiners
{
    public class JoinerFlow : IThuisbezorgdFlow
    {
        private HashSet<long> _joiners = new HashSet<long>();

        public async Task Init(ChatState state, TelegramBotClient bot, Message message)
        {
            await bot.SendTextMessageAsync(state.ChatId, "Who wants to join for dinner today?, type !join to join");
        }

        public async Task<bool> ProcessMessage(ChatState state, TelegramBotClient bot, Message message)
        {
            if (message.Text.Equals("!join", StringComparison.OrdinalIgnoreCase))
            {
                _joiners.Add(message.From.Id);
                var name = state.GetName(message.From.Id);
                await bot.SendTextMessageAsync(state.ChatId, $"Thanks {name} for joining. You will be pleased by me, your friendly food guzzler.");
            }
            else if (message.Text.Equals("!go", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public async Task Finish(ChatState state, TelegramBotClient bot, Message message)
        {
            await bot.SendTextMessageAsync(state.ChatId, $"So we have:{Environment.NewLine}{string.Join(Environment.NewLine, _joiners.Select(t => state.GetName(t)))}");

            state.CurrentState.FoodSmikkelaars.AddRange(_joiners.Select(t => new UserFoodSmikkeltje()
            {
                UserId = t
            }));
        }
    }
}
