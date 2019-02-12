using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherBot.Models;

namespace WeatherBot
{
    public class WeatherDataSource
    {
        public readonly string _apiKey;
        private readonly HttpClient httpClient;

        public WeatherDataSource(string apiKey)
        {
            _apiKey = apiKey;
            httpClient = new HttpClient();
            
        }
        public string GetLocationKey(string SearchText)
        {
            httpClient.BaseAddress = new Uri("http://dataservice.accuweather.com/locations/v1/search");
            string parameters = "?q="+SearchText+"&apikey="+_apiKey;
            HttpResponseMessage response = httpClient.GetAsync(parameters).Result;
            if (response.IsSuccessStatusCode)
            {
                JArray ar = JArray.Parse(response.Content.ToString());
                if(ar.Count >0)
                    return ar.First.Value<string>("Key").ToString();
            }
            throw new KeyNotFoundException();
            
        }
        public WeatherData GetWeatherData(string locationKey)
        {
            httpClient.BaseAddress = new Uri("http://dataservice.accuweather.com/currentconditions/v1/"+locationKey);
            string parameters = "?apikey=" + _apiKey;
            HttpResponseMessage response = httpClient.GetAsync(parameters).Result;
            if (response.IsSuccessStatusCode)
            {
                JArray ar = JArray.Parse(response.Content.ToString());
                if (ar.Count > 0)
                    return ar.First.ToObject<WeatherData>();
            }
            throw new KeyNotFoundException();
        }
    }
}
