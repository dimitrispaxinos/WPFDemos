using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace ProgressChartSample
{
    public class FlowChartControl : UserControl
    {
        private StackPanel StackPanel = new StackPanel() { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = System.Windows.HorizontalAlignment.Center };


        private int transitionDuration = 500;
        private int MainOpacity = 0;

        public Type AvailableStatuses
        {
            get { return (Type)GetValue(AvailableStatusesProperty); }
            set { SetValue(AvailableStatusesProperty, value); }
        }

        public static readonly DependencyProperty AvailableStatusesProperty = DependencyProperty.Register(
            "AvailableStatuses",
            typeof(Type),
            typeof(FlowChartControl));
        public bool Animate
        {
            get { return (bool)GetValue(AnimateProperty); }
            set { SetValue(AnimateProperty, value); }
        }

        public static readonly DependencyProperty AnimateProperty = DependencyProperty.Register(
            "Animate",
            typeof(bool),
            typeof(FlowChartControl));
        public object CurrentStatus
        {
            get { return (object)GetValue(CurrentStatusProperty); }
            set { SetValue(CurrentStatusProperty, value); }
        }

        public static readonly DependencyProperty CurrentStatusProperty = DependencyProperty.Register(
            "CurrentStatus",
            typeof(object),
            typeof(FlowChartControl),
            new PropertyMetadata(CurrentStatusCahnagedCallBack));

        public FlowChartControl()
        {
            AddChild(StackPanel);
        }

        public static IEnumerable<object> GetEnumObjects(Type enumType)
        {
            var values = Enum.GetNames(enumType);
            var list = new List<object>();

            foreach (var value in values)
            {
                list.Add(Enum.Parse(enumType, value));
            }
            return list;
        }

        private static void CurrentStatusCahnagedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = d as FlowChartControl;
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

        private void CreateChart()
        {
            var statuses = GetEnumObjects(AvailableStatuses);

            foreach (var status in statuses)
            {
                var circle = new FlowChartEllipse() { Opacity = MainOpacity };
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
                    var line = new FlowChartLine() { Opacity = MainOpacity };
                    line.Name = status + "Line";
                    if ((int)CurrentStatus > (int)status)
                        line.IsPast = true;
                    StackPanel.Children.Add(line);
                }
            }
        }

        private Storyboard CreateStoryBoard()
        {
            var sb = new Storyboard();

            var beginTime = 0;
            foreach (var child in StackPanel.Children)
            {
                if (child as Button != null)
                    continue;

                var animation = new DoubleAnimation()
                {
                    BeginTime = TimeSpan.FromMilliseconds(beginTime),
                    Duration = TimeSpan.FromMilliseconds(transitionDuration),
                    From = 0,
                    To = 1
                };
                animation.From = 0;
                animation.To = 1;
                beginTime = beginTime + transitionDuration;
                Storyboard.SetTarget(animation, (UserControl)child);
                Storyboard.SetTargetProperty(animation, new PropertyPath(OpacityProperty));
                sb.Children.Add(animation);
            }
            return sb;
        }
    }
}
