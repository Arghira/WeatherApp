using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;

namespace WeatherApp.ViewModel.Helpers
{
    public class AccuWeatherHelper
    {
        public const string base_url = "http://dataservice.accuweather.com/";
        public const string autoComplete_endpoint = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
        public const string current_condition_endpoint = "currentconditions/v1/{0}?apikey={1}";
        public const string apiKey = "QhqW6ksVQNTfBsBB6RzOdhcE6RUiFz1W";

        public static async Task<List<City>> GetCities(string query)
        {
            var url = $"{base_url}{string.Format(autoComplete_endpoint, apiKey, query)}";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode(); // Ensure that the HTTP request was successful.

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<City>>(json);
            }
        }

        public static async Task<CurrentConditions> GetCurrentConditionsAsync(string cityKey)
        {
            var url = $"{base_url}{string.Format(current_condition_endpoint, cityKey, apiKey)}";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode(); // Ensure that the HTTP request was successful.

                var json = await response.Content.ReadAsStringAsync();
                return (JsonConvert.DeserializeObject<List<CurrentConditions>>(json)).FirstOrDefault();
            }
        }
    }

}
