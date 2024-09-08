using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Oasis_Visual_Pipelines.Converters
{
    public class HeightExpanderButtonVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[2] is bool isExpanded && isExpanded)
                return Visibility.Visible;

            if (!double.TryParse(values[0]?.ToString(), out double actualHeight)
                || !double.TryParse(values[1]?.ToString(), out double maxHeight))
                return Visibility.Collapsed;

            if (maxHeight is double.PositiveInfinity) return Visibility.Visible;

            return actualHeight >= maxHeight ? Visibility.Visible : Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
