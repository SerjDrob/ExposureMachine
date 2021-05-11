using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ExposureMachine.View;
using ExposureMachine.ViewModel;

namespace ExposureMachine
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            new MainView() { DataContext = new MainViewModel() }.Show();
        }       
    }
}
