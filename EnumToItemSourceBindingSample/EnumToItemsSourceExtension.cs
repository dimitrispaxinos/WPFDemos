using System;
using System.Windows.Markup;

namespace EnumToItemSourceBindingSample
{
    public class EnumToItemsSourceExtension : MarkupExtension
    {
        private readonly Type _type;

        public EnumToItemsSourceExtension(Type type)
        {
            _type = type;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var valuesList = Enum.GetValues(_type);
            return valuesList;
        }
    }
}
