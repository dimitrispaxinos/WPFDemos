using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace ProgressChartSample
{
    public class ProgressChartControl : UserControl
    {
        private StackPanel StackPanel = new StackPanel() { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = System.Windows.HorizontalAlignment.Center };

        private int MainOpacity = 0;

        public Type StatusEnum
        {
            get { return (Type)GetValue(StatusEnumProperty); }
            set { SetValue(StatusEnumProperty, value); }
        }

        public static readonly DependencyProperty StatusEnumProperty = DependencyProperty.Register("StatusEnum", typeof(Type), typeof(ProgressChartControl));
        public bool Animate
        {
            get { return (bool)GetValue(AnimateProperty); }
            set { SetValue(AnimateProperty, value); }
        }

        public static readonly DependencyProperty AnimateProperty = DependencyProperty.Register("Animate", typeof(bool), typeof(ProgressChartControl));
        public object CurrentStatus
        {
            get { return (object)GetValue(CurrentStatusProperty); }
            set { SetValue(CurrentStatusProperty, value); }
        }

        public static readonly DependencyProperty CurrentStatusProperty = DependencyProperty.Register("CurrentStatus", typeof(object), typeof(ProgressChartControl), new PropertyMetadata(CurrentStatusCahnagedCallBack));

        private static void CurrentStatusCahnagedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = d as ProgressChartControl;
            if (instance != null)
            {
                instance.StackPanel.Children.Clear();
                if (instance.Animate)
                {
                    instance.CreateChart();
                    var sb = instance.CreateStoryBoard();
                    sb.Begin();
                }
                else
                {
                    instance.MainOpacity = 1;
                    instance.CreateChart();
                }
            }
        }

        public ProgressChartControl()
        {
            this.AddChild(StackPanel);
        }

        private void CreateChart()
        {
            var statuses = GetEnumObjects(StatusEnum);

            foreach (var status in statuses)
            {
                var circle = new ProgressChartEllipse() { Opacity = MainOpacity };
                circle.Content = status.ToString();
                circle.Name = status.ToString();

                if ((int)CurrentStatus > (int)status)
                    circle.Status = "IsPast";
                if ((int)CurrentStatus == (int)status)
                    circle.Status = "IsCurrent";
                if ((int)CurrentStatus < (int)status)
                    circle.Status = "IsFuture";

                StackPanel.Children.Add(circle);

                if ((int)status != statuses.Cast<int>().Max())
                {
                    var line = new ProgressChartLine() { Opacity = MainOpacity };
                    line.Name = status + "Line";
                    if ((int)CurrentStatus > (int)status)
                        line.IsPast = true;
                    StackPanel.Children.Add(line);
                }
            }
        }

        private Storyboard CreateStoryBoard()
        {
            var duration = 500;

            var sb = new Storyboard();

            var beginTime = 0;
            foreach (var child in StackPanel.Children)
            {
                if (child as Button != null)
                    continue;

                var animation = new DoubleAnimation()
                {
                    BeginTime = TimeSpan.FromMilliseconds(beginTime),
                    Duration = TimeSpan.FromMilliseconds(duration),
                    From = 0,
                    To = 1
                };
                animation.From = 0;
                animation.To = 1;
                beginTime = beginTime + duration;
                Storyboard.SetTarget(animation, (UserControl)child);
                Storyboard.SetTargetProperty(animation, new PropertyPath(OpacityProperty));
                sb.Children.Add(animation);
            }
            return sb;
        }

        public static IEnumerable<object> GetEnumObjects(Type enumType)
        {
            var values = Enum.GetNames(enumType);
            List<object> list = new List<object>();

            foreach (var value in values)
            {
                list.Add(Enum.Parse(enumType, value));
            }
            return list;
        }
    }
}
