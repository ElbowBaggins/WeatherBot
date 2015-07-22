using System;
using Hemogoblin.ChatBot;

namespace Hemogoblin.WeatherBot
{
    public class WeatherBot
    {
        /// <summary>
        /// Super lazy Main method.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            IChatBotConfig botConfig;
            try
            {
                // Try to open the config file (hardcoded to config.dat because I said so)
                botConfig = WeatherBotConfig.OpenConfigFile();
            }   // If it doesn't open, just make a new one. Yes, catch-alls are bad. It's a lazy Main.
            catch (Exception)
            {
                botConfig = WeatherBotConfig.GenerateConfig();
                try
                {
                    ((WeatherBotConfig) botConfig).SaveConfigFile();
                } // If we cant' save for some reason, just ignore it. It's not like we can't do this again.
                catch (Exception)
                {
                    Console.WriteLine("Could not save configuration.");
                }
            }
            // Start a ChatBot instance with a YWeatherListener that triggers on ".w"
            var bot = new ChatBot.ChatBot(botConfig);
            bot.RegisterListener(new YWeatherListener(".w"));
            bot.Go();
        }
    }
}
