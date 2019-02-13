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
        private HttpClient httpClient;

        public WeatherDataSource(string apiKey)
        {
            _apiKey = apiKey;

        }
        public string GetLocationKey(string SearchText, out string responseCode)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://dataservice.accuweather.com/locations/v1/search");
            string parameters = "?q="+SearchText+"&apikey="+_apiKey;
            string locationKey = null;
            HttpResponseMessage response = httpClient.GetAsync(parameters).Result;
            responseCode = response.StatusCode.ToString();
            if (response.IsSuccessStatusCode)
            {
                
                JArray ar = JArray.Parse(response.Content.ReadAsStringAsync().Result);
                if(ar.Count >0)
                    locationKey = ar.First.Value<string>("Key").ToString();
            }
            httpClient.Dispose();
            httpClient = null;
            return locationKey;
            
        }
        public WeatherData GetWeatherData(string locationKey, out string responseCode)
        {
            if(locationKey == null)
                throw new KeyNotFoundException();

            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(@"http://dataservice.accuweather.com");
            WeatherData data = null;
            string parameters = "/currentconditions/v1/" +locationKey + "/"+"?apikey=" + _apiKey;
            HttpResponseMessage response = httpClient.GetAsync(parameters).Result;
            responseCode = response.StatusCode.ToString();
            if (response.IsSuccessStatusCode)
            {
                JArray ar = JArray.Parse(response.Content.ReadAsStringAsync().Result);
                if (ar.Count > 0)
                {
                    //data = ar.First.ToObject<WeatherData>();
                    string datetime = ar.First["LocalObservationDateTime"].Value<string>();
                    string text = ar.First["WeatherText"].Value<string>();
                    JObject temp = ar.First["Temperature"].Value<JObject>();
                    JObject metric = (JObject)temp["Metric"];
                    string tempC = (string)metric["Value"];
                    data = new WeatherData(datetime, text, tempC);
                }
                    
                else
                    throw new KeyNotFoundException();
            }

            httpClient.Dispose();
            httpClient = null;
            return data;
        }
    }
}
