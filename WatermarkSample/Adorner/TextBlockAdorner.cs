using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WatermarkSample.Adorner
{
    public class TextBlockAdorner : System.Windows.Documents.Adorner
    {
        public readonly TextBlock adornerTextBlock;

        //public TextBlockAdorner(UIElement adornedElement, string label, Style labelStyle)
        //    : base(adornedElement)
        //{
        //    adornerTextBlock = new TextBlock { Style = labelStyle, Text = label };
        //}

        public TextBlockAdorner(UIElement adornedElement, Style labelStyle)
            : base(adornedElement)
        {
            adornerTextBlock = new TextBlock { Style = labelStyle };
        }

        protected override Size MeasureOverride(Size constraint)
        {
            adornerTextBlock.Measure(constraint);
            return constraint;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            adornerTextBlock.Arrange(new Rect(finalSize));
            return finalSize;
        }

        protected override Visual GetVisualChild(int index)
        {
            return adornerTextBlock;
        }

        protected override int VisualChildrenCount
        {
            get { return 1; }
        }
    }
}
