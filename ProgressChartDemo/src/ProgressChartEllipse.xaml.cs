using System.Windows;
using System.Windows.Controls;

namespace ProgressChartSample
{
    /// <summary>
    /// Interaction logic for ProgressChartEllipse.xaml
    /// </summary>
    public partial class ProgressChartEllipse : UserControl
    {
        public string Status
        {
            get { return (string)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        public static readonly DependencyProperty StatusProperty = DependencyProperty.Register("Status", typeof(string), typeof(ProgressChartEllipse));

        public string Content
        {
            get { return (string)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(string), typeof(ProgressChartEllipse));

        public ProgressChartEllipse()
        {
            InitializeComponent();
            Loaded += ProgressCircle_Loaded;
        }

        void ProgressCircle_Loaded(object sender, RoutedEventArgs e)
        {
            var width = Canvas.ActualWidth;
            double left = (Canvas.ActualWidth - TextBlock.ActualWidth) / 2;
            Canvas.SetLeft(TextBlock, left);
        }
    }
}
