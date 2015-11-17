using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SampleNavigationApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            var window = new MainWindow();
            
            var viewNavigator = new ViewNavigator(window);

            var mainWindowViewModel = new MainWindowViewModel(viewNavigator);

            window.DataContext = mainWindowViewModel;
            
            Current.MainWindow = window;

            Current.MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
