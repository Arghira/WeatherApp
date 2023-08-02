using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.Model;

namespace WeatherApp.ViewModel.Helpers
{
    public interface IAccuWeatherHelper
    {
        Task<List<City>> GetCities(string query);
        Task<CurrentConditions> GetCurrentConditionsAsync(string cityKey);
    }

}
