using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ExposureMachine.Classes;
using ExposureMachine.View;
using ExposureMachine.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace ExposureMachine
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ServiceCollection MainIoC { get; private set; }
        public App()
        {
            MainIoC = new ServiceCollection();

            MainIoC.AddScoped<IVideoCapture, USBCamera>()
                .AddScoped<IVideoCapture, USBCamera>()
                .AddSingleton<MainViewModel>();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = MainIoC.BuildServiceProvider();
            var viewModel = builder.GetService<MainViewModel>();
            base.OnStartup(e);
            new MainView() { DataContext = viewModel }.Show();
        }       
    }
}
