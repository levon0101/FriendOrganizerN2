using FriendOrganizerN2.UI.ViewModel;
using MahApps.Metro.Controls;
using System.Windows;

namespace FriendOrganizerN2.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private MainViewModel _viewModel;

        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _viewModel.MainWindowView = this;
            DataContext = _viewModel;

            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
           await  _viewModel.LoadAsync();
        }


    }
}
