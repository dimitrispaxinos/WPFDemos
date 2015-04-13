using System.Windows;
using System.Windows.Controls;

namespace ProgressChartSample
{
    /// <summary>
    /// Interaction logic for FlowChartEllipse.xaml
    /// </summary>
    public partial class FlowChartEllipse
    {
        public string Status
        {
            get { return (string)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        public static readonly DependencyProperty StatusProperty = DependencyProperty.Register(
            "Status",
            typeof(string),
            typeof(FlowChartEllipse));

        public string Content
        {
            get { return (string)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
            "Content",
            typeof(string),
            typeof(FlowChartEllipse));

        public FlowChartEllipse()
        {
            InitializeComponent();
            Loaded += ProgressCircle_Loaded;
        }

        private void ProgressCircle_Loaded(object sender, RoutedEventArgs e)
        {
            double left = (Canvas.ActualWidth - TextBlock.ActualWidth) / 2;
            Canvas.SetLeft(TextBlock, left);
        }
    }
}
