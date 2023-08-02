using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using WeatherApp.ViewModel;

namespace WeatherApp.View
{
    /// <summary>
    /// Interaction logic for WeatherWindow.xaml
    /// </summary>
    public sealed partial class WeatherWindow : Window
    {
        private readonly IServiceProvider serviceProvider;
        public WeatherWindow()
        {
            InitializeComponent();

            //DataContext = Ioc.Default.GetRequiredService<WeatherViewModel>();
            DataContext = App.Current.Services.GetService<WeatherViewModel>();
        }

        public WeatherViewModel ViewModel => (WeatherViewModel)DataContext;
    }
}
