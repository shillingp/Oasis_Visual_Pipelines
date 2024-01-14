using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace Oasis_Visual_Pipelines.Converters
{
    internal class EnumDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum enumValue)
            {
                if (enumValue.GetType().GetField(enumValue.ToString()) is FieldInfo field
                    && field.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes 
                    && attributes.Any())
                    return attributes.First().Description;

                return enumValue.ToString();
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
