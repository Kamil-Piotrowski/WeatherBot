using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherBot
{
    public static class SearchText_SyntaxFilter
    {
        private static JObject locations;
        public static string GetLocationName(string searchText)
        {
            if (locations == null)
                initLocationsFromFile();

            try
            {
                return (string)locations[searchText];
            }
            catch
            {
                return searchText;
            }
           
        }
        private static void initLocationsFromFile()
        {
            using (StreamReader r = new StreamReader(@"locations.json"))
            { 
                locations = JObject.Parse(r.ReadToEnd());
            }
        }
    }
}
