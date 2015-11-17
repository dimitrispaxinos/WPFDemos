using System.Windows;

namespace WatermarkSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindowViewModel MainWindowViewModel
        {
            get
            {
                return DataContext as MainWindowViewModel;
            }
        }

        private void ChangeWatermarkClick(object sender, RoutedEventArgs e)
        {
            if (MainWindowViewModel != null)
            {
                MainWindowViewModel.ChangeWatermarkTexts();
            }
        }
    }
}
