using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WeatherApp.Model;
using WeatherApp.ViewModel.Commands;
using WeatherApp.ViewModel.Helpers;

namespace WeatherApp.ViewModel
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        public SearchCommand SearchCommand { get; set; }
        public ObservableCollection<City> Cities { get; set; }

        private string query;
        private City _selectedCity;
        private CurrentConditions currentConditions;
        public event PropertyChangedEventHandler PropertyChanged;
        private bool isGettingConditions = false;

        public WeatherViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                _selectedCity = new City
                {
                    LocalizedName = "Romania"
                };
                currentConditions = new CurrentConditions
                {
                    WeatherText = "Partly cloudy",
                    Temperature = new Temperature
                    {
                        Metric = new Units
                        {
                            Value = "21"
                        }
                    }
                };
            }

            SearchCommand = new SearchCommand(this);
            Cities = new ObservableCollection<City>();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Query
        {
            get { return query; }
            set
            {
                query = value;
                OnPropertyChanged("Query");
            }
        }

        public async void MakeQuery()
        {
            var cities = await AccuWeatherHelper.GetCities(Query);

            Cities.Clear();
            foreach (var city in cities) 
            {
                Cities.Add(city);
            }
        }

        public void ClearCities()
        {
            Cities?.Clear();
        }

        public City SelectedCity
        {
            get { return _selectedCity; }
            set
            {
                if (_selectedCity != value && !isGettingConditions)
                {
                    _selectedCity = value;
                    OnPropertyChanged("SelectedCity");
                    isGettingConditions = true;
                    GetCurrentConditions();
                    isGettingConditions = false;
                }
            }
        }

        private async void GetCurrentConditions()
        {
            Query = string.Empty;
            ClearCities();
            CurrentConditions = await AccuWeatherHelper.GetCurrentConditionsAsync(SelectedCity.Key);
        }

        public CurrentConditions CurrentConditions
        {
            get { return currentConditions; }
            set
            {
                currentConditions = value;
                OnPropertyChanged("CurrentConditions");
            }
        }
    }
}
