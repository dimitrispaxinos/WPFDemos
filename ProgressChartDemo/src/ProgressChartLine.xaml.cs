using System.Windows;
using System.Windows.Controls;

namespace ProgressChartSample
{
    /// <summary>
    /// Interaction logic for ProgressChartLine.xaml
    /// </summary>
    public partial class ProgressChartLine : UserControl
    {
        public bool IsPast
        {
            get { return (bool)GetValue(IsPastProperty); }
            set { SetValue(IsPastProperty, value); }
        }

        public static readonly DependencyProperty IsPastProperty = DependencyProperty.Register("IsPast", typeof(bool), typeof(ProgressChartLine));

        public ProgressChartLine()
        {
            InitializeComponent();
        }
    }
}
