using System;
using System.Linq;
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
            var list = Enum.GetValues(_type)
                .Cast<object>()
                .Select(e => new { Value = Convert.ChangeType(e, _type) });

            return list;
        }
    }
}
