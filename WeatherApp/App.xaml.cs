using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using WeatherApp.View;
using WeatherApp.ViewModel;

namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    sealed partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();

            this.InitializeComponent();
        }

        public new static App Current => (App)Application.Current;

        public IServiceProvider Services { get; }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            //Viewmodels
            services.AddTransient<IWeatherViewModel, WeatherViewModel>();
            services.AddTransient<WeatherWindow>();

            return services.BuildServiceProvider();
        }
    }
}
