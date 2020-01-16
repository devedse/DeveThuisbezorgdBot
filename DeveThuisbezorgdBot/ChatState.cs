using DeveCoolLib.Logging;
using DeveCoolLib.Threading;
using DeveThuisbezorgdBot.Flows;
using DeveThuisbezorgdBot.Flows.Joiners;
using DeveThuisbezorgdBot.State;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace DeveThuisbezorgdBot
{
    public class ChatState
    {
        public ThuisbezorgdState CurrentState { get; set; }
        public IThuisbezorgdFlow CurrentFlow { get; set; }

        public long ChatId { get; }

        public Dictionary<long, int> Points = new Dictionary<long, int>();
        private readonly ILogger _logger;
        private readonly GlobalBotState _globalBotState;

        private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);

        public ChatState(ILogger logger, GlobalBotState globalBotState, long chatId)
        {
            _logger = logger;
            _globalBotState = globalBotState;
            ChatId = chatId;
        }

        public async Task HandleMessage(TelegramBotClient bot, Message message)
        {
            //Ensure only one message can be handled at a time
            using (var disposableSemaphore = await _semaphoreSlim.DisposableWaitAsync())
            {
                var msg = message.Text;

                if (msg.Equals("!food"))
                {
                    CurrentState = new ThuisbezorgdState()
                    {
                        Initiator = message.From.Id
                    };
                    CurrentFlow = null;

                    await GoToNextFlow(bot, message);
                }
                else if (CurrentFlow != null)
                {
                    var finished = await CurrentFlow.ProcessMessage(this, bot, message);
                    if (finished)
                    {
                        await GoToNextFlow(bot, message);
                    }
                }
            }
        }

        private async Task GoToNextFlow(TelegramBotClient bot, Message message)
        {
            if (CurrentFlow != null)
            {
                await CurrentFlow.Finish(this, bot, message);
            }
            CurrentFlow = DetermineNextFlow();
            await CurrentFlow.Init(this, bot, message);
        }

        public IThuisbezorgdFlow DetermineNextFlow()
        {
            if (CurrentState.FoodSmikkelaars.Count == 0)
            {
                return new JoinerFlow();
            }
            else
            {
                return new RestaurantSelectionFlow();
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
