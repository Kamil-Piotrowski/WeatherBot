// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using WeatherBot.Models;

namespace WeatherBot
{
    
    public class WeatherBotBot : IBot
    {
        private readonly WeatherBotAccessors _accessors;
        private readonly ILogger _logger;
        private readonly WeatherDataSource _weatherDataSource;

        
        public WeatherBotBot(WeatherBotAccessors accessors, ILoggerFactory loggerFactory, WeatherDataSource weatherDataSource)
        {
            if (loggerFactory == null)
            {
                throw new System.ArgumentNullException(nameof(loggerFactory));
            }
            _weatherDataSource = weatherDataSource;
            _logger = loggerFactory.CreateLogger<WeatherBotBot>();
            _logger.LogTrace("Turn start.");
            _accessors = accessors ?? throw new System.ArgumentNullException(nameof(accessors));
        }

       
        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
           
            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                await _accessors.ConversationState.SaveChangesAsync(turnContext);

                string searchText = BotLogic.getSearchText(turnContext.Activity.Text);
                string responseMessage = BotLogic.getWeatherData(searchText, _weatherDataSource);
                responseMessage = SearchText_SyntaxFilter.GetLocationName(responseMessage);
                await turnContext.SendActivityAsync(responseMessage);
            }
            else
            {
                foreach (ChannelAccount acc in turnContext.Activity.MembersAdded)
                {
                    if (acc.Id == turnContext.Activity.Recipient.Id)
                    {
                        await turnContext.SendActivityAsync("Hello! Im weatherbot!");
                        break;
                    }
                }
            }
        }

        
    }
    public static class BotLogic
    {
        public static string getSearchText(string requesterStatement)
        {
            requesterStatement = requesterStatement
                .Replace("?", "")
                .Replace(".", "")
                .Replace(",", "")
                .Replace("/", "")
                .Replace("!", "")
                .Replace("\\", "");

            var words = requesterStatement.Split(' ');
            return (words[words.Length - 1]);
        }
        public static string getWeatherData(string searchText, WeatherDataSource _weatherDataSource)
        {
            string responseCode = null;
            try
            {

                string locationKey = _weatherDataSource.GetLocationKey(searchText, out responseCode);
                WeatherData res = _weatherDataSource.GetWeatherData(locationKey, out responseCode);
                return "Time: " + res.LocalTime + ", Description: " + res.Description + ", temperature: " + res.TemperatureCelsius + " C";
            }
            catch
            {
                if (responseCode != null)
                    return "oops, " + responseCode;
                return "something went wrong";
            }

        }
    }
}
