using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace Oasis_Pipelines.Operations.Wpf.Converters;

public sealed class EnumDescriptionConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Enum enumValue)
        {
            if (enumValue.GetType().GetField(enumValue.ToString()) is { } field
                && field.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes
                && attributes.Any())
                return attributes.First().Description;

            return enumValue.ToString();
        }

        return Binding.DoNothing;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}