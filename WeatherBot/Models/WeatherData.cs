using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherBot.Models
{
    public class WeatherData
    {
        public WeatherData(string LocalObservationDateTime, string WeatherText, string Temperature)
        {
            _localDateTime = DateTime.Parse(LocalObservationDateTime);
            Description = WeatherText;
            TemperatureCelsius = double.Parse(Temperature);
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
        public double TemperatureCelsius { get; }
    }
}
