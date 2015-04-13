using System.Windows;
using System.Windows.Controls;

namespace ProgressChartSample
{
    /// <summary>
    /// Interaction logic for FlowChartLine.xaml
    /// </summary>
    public partial class FlowChartLine : UserControl
    {
        public bool IsPast
        {
            get { return (bool)GetValue(IsPastProperty); }
            set { SetValue(IsPastProperty, value); }
        }

        public static readonly DependencyProperty IsPastProperty = DependencyProperty.Register("IsPast", typeof(bool), typeof(FlowChartLine));

        public FlowChartLine()
        {
            InitializeComponent();
        }
    }
}
