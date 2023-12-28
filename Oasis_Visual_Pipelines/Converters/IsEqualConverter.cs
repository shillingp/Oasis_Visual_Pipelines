using System.Globalization;
using System.Windows.Data;

namespace Oasis_Visual_Pipelines.Converters
{
    internal class IsEqualConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.GetHashCode() == parameter?.GetHashCode();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? parameter : Binding.DoNothing;
        }
    }
}
