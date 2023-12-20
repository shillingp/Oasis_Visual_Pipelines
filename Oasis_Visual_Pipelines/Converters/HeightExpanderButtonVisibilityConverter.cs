using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Oasis_Visual_Pipelines.Controls
{
    public class HeightExpanderButtonVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2) return Visibility.Collapsed;

            double.TryParse(values[0]?.ToString(), out double actualHeight);
            double.TryParse(values[1]?.ToString(), out double maxHeight);

            if (maxHeight is double.PositiveInfinity) return Visibility.Visible;

            return actualHeight >= maxHeight ? Visibility.Visible : Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
