﻿namespace DeveThuisbezorgdBot.Config
{
    public class BotConfig
    {
        public string TelegramBotToken { get; set; }
        
        public string Postcode { get; set; }

        public bool IsValid =>
            !string.IsNullOrWhiteSpace(TelegramBotToken);
    }
}
