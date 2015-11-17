using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;

using WatermarkSample.Adorner;
using WatermarkSample.Events;

namespace WatermarkSample.Behaviors
{
    public class PasswordBoxWatermarkBehavior : Behavior<PasswordBox>
    {
        private TextBlockAdorner adorner;
        private WeakPropertyChangeNotifier notifier;

        #region DependencyProperty's

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.RegisterAttached("Label", typeof(string), typeof(PasswordBoxWatermarkBehavior));

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty LabelStyleProperty =
            DependencyProperty.RegisterAttached("LabelStyle", typeof(Style), typeof(PasswordBoxWatermarkBehavior));

        public Style LabelStyle
        {
            get { return (Style)GetValue(LabelStyleProperty); }
            set { SetValue(LabelStyleProperty, value); }
        }

        #endregion

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += AssociatedObjectLoaded;
            AssociatedObject.PasswordChanged += AssociatedObjectTextChanged;
            AssociatedObject.IsVisibleChanged += AssociatedObjectIsVisibleChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Loaded -= AssociatedObjectLoaded;
            AssociatedObject.PasswordChanged -= AssociatedObjectTextChanged;
            AssociatedObject.IsVisibleChanged -= AssociatedObjectIsVisibleChanged;

            notifier = null;
        }

        private void AssociatedObjectIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateAdorner();
        }

        private void AssociatedObjectTextChanged(object sender, RoutedEventArgs e)
        {
            UpdateAdorner();
        }

        private void AssociatedObjectLoaded(object sender, RoutedEventArgs e)
        {
            adorner = new TextBlockAdorner(AssociatedObject, LabelStyle);

            // Set binding in code so that it updates everytime 
            Binding binding = new Binding();
            binding.Source = this;
            binding.Path = new PropertyPath("Label");
            adorner.adornerTextBlock.SetBinding(TextBlock.TextProperty, binding);

            UpdateAdorner();

            //AddValueChanged for IsFocused in a weak manner
            notifier = new WeakPropertyChangeNotifier(AssociatedObject, UIElement.IsFocusedProperty);
            notifier.ValueChanged += UpdateAdorner;
        }

        private void UpdateAdorner(object sender, EventArgs e)
        {
            UpdateAdorner();
        }


        private void UpdateAdorner()
        {
            if (!String.IsNullOrEmpty(AssociatedObject.Password) || AssociatedObject.IsFocused || AssociatedObject.Visibility == Visibility.Collapsed)
            {
                // Hide the Watermark Label if the adorner layer is visible
                AssociatedObject.TryRemoveAdorners<TextBlockAdorner>();
            }
            else
            {
                // Show the Watermark Label if the adorner layer is visible
                if (adorner != null)
                    AssociatedObject.TryAddAdorner<TextBlockAdorner>(adorner);
            }
        }
    }
}
