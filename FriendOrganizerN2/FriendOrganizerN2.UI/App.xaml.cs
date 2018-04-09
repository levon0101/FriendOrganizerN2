using FriendOrganizerN2.UI.Data;
using FriendOrganizerN2.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FriendOrganizerN2.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var mainWindow = new MainWindow
                (new MainViewModel(
                new FriendDataService()));
            mainWindow.Show();
        }
    }
}
