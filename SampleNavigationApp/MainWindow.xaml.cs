using System.Windows;

namespace SampleNavigationApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel mainWindowViewModel)
            :this()
        {
            DataContext = mainWindowViewModel;
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
