using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;

using WatermarkSample.Adorner;
using WatermarkSample.Events;

namespace WatermarkSample.Behaviors
{
    public class ComboBoxWatermarkBehavior : Behavior<ComboBox>
    {
        private TextBlockAdorner adorner;
        private WeakPropertyChangeNotifier notifier;

        #region DependencyProperty's

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.RegisterAttached("Label", typeof(string), typeof(ComboBoxWatermarkBehavior));

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty LabelStyleProperty =
            DependencyProperty.RegisterAttached("LabelStyle", typeof(Style), typeof(ComboBoxWatermarkBehavior));

        public Style LabelStyle
        {
            get { return (Style)GetValue(LabelStyleProperty); }
            set { SetValue(LabelStyleProperty, value); }
        }

        #endregion

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Text = Label;
            this.AssociatedObject.Loaded += this.AssociatedObjectLoaded;
            this.AssociatedObject.GotFocus += this.AssociatedObjectTextChanged;
            this.AssociatedObject.LostFocus += this.LostFocusEventHandler;
        }

        private void LostFocusEventHandler(object sender, RoutedEventArgs e)
        {
            UpdateAdorner();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.Loaded -= this.AssociatedObjectLoaded;
            this.AssociatedObject.GotFocus -= this.AssociatedObjectTextChanged;
            this.notifier = null;
        }

        private void AssociatedObjectTextChanged(object sender, RoutedEventArgs e)
        {
            UpdateAdorner();
        }

        private void AssociatedObjectLoaded(object sender, RoutedEventArgs e)
        {
            this.adorner = new TextBlockAdorner(this.AssociatedObject, this.LabelStyle);

            // Set binding in code so that it updates everytime 
            Binding binding = new Binding();
            binding.Source = this;
            binding.Path = new PropertyPath("Label");
            adorner.adornerTextBlock.SetBinding(TextBlock.TextProperty, binding);

            this.UpdateAdorner();

            //AddValueChanged for IsFocused in a weak manner
            this.notifier = new WeakPropertyChangeNotifier(this.AssociatedObject, UIElement.IsFocusedProperty);
            this.notifier.ValueChanged += new EventHandler(this.UpdateAdorner);
        }

        private void UpdateAdorner(object sender, EventArgs e)
        {
            this.UpdateAdorner();
        }


        private void UpdateAdorner()
        {
            if (this.AssociatedObject.SelectedItem != null || this.AssociatedObject.IsFocused)
            {
                // Hide the Watermark Label if the adorner layer is visible
                this.AssociatedObject.TryRemoveAdorners<TextBlockAdorner>();
            }
            else
            {
                // Show the Watermark Label if the adorner layer is visible
                this.AssociatedObject.TryAddAdorner<TextBlockAdorner>(adorner);
            }
        }
    }
}
