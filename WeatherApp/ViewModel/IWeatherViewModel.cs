using System.Collections.ObjectModel;
using System.ComponentModel;
using WeatherApp.Model;
using WeatherApp.ViewModel.Commands;

namespace WeatherApp.ViewModel
{
    public interface IWeatherViewModel : INotifyPropertyChanged
    {
        SearchCommand SearchCommand { get; set; }
        ObservableCollection<City> Cities { get; set; }
        string Query { get; set; }
        void MakeQuery();
        void ClearCities();
        City SelectedCity { get; set; }
        CurrentConditions CurrentConditions { get; set; }
    }
}