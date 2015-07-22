using System.Threading.Tasks;
using Hemogoblin.ChatBot;
using Hemogoblin.YWeather;

namespace Hemogoblin.WeatherBot
{
    class YWeatherListener : IChatBotResponder
    {
        /// <summary>
        /// Grabs a WeatherData from the YWeather library and makes it formatted all pretty so it doesn't 
        /// just *totally* bungle everyone's IRC logs.
        /// </summary>
        /// <param name="triggerMessage">The message which triggered this response</param>
        /// <returns>A response</returns>
        public string GetResponse(string triggerMessage)
        {
            // Get a WeatherData. The method used is an async method so to use it here we have to wrap it in this
            // Task.Run() silliness.
            var weatherData = (Task.Run(async() => await WeatherData.GetWeatherAsync(triggerMessage.Replace(Trigger, "")))).Result;
            string weatherString;
            if (!weatherData.IsComplete)
            {
                weatherString = "Complete weather data could not be retrieved for the given location.";
            }
            else
            {
                weatherString = "Conditions for " + weatherData.Location + ": " + weatherData.Conditions +
                                " -- Temperature: " + weatherData.Temperature + " -- Wind Speed: " + weatherData.WindSpeed;
                if (weatherData.WindSpeed != "Still")
                {
                    weatherString += " -- Wind Direction: " + weatherData.WindDirection +
                                     " -- Wind Chill: " + weatherData.WindChill;
                }
                weatherString += " -- Air Pressure: " + weatherData.Pressure +
                                 " -- Humidity: " + weatherData.Humidity +
                                 " -- Visibility: " + weatherData.Visibility +
                                 " -- Sunrise: " + weatherData.Sunrise +
                                 " -- Sunset: " + weatherData.Sunset +
                                 " -- Last Updated: " + weatherData.TimeUpdated;
            }
            return weatherString;
        }

        public string Trigger { get; private set; }

        public YWeatherListener(string trigger)
        {
            Trigger = trigger + " ";
        }
    }
}
