using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherBot.Models
{
    public class WeatherData
    {
        public WeatherData(DateTime localDateTime, string description, int temperatureCelsius)
        {
            _localDateTime = localDateTime;
            Description = description;
            TemperatureCelsius = temperatureCelsius;
        }

        private readonly DateTime _localDateTime;
        public string LocalTime
        {
            get
            {
                return _localDateTime.Hour + ":" + _localDateTime.Minute;
            }
        }
        public string Description { get; }
        public int TemperatureCelsius { get; }
    }
}
