using System.Globalization;
using System.Windows.Data;

namespace Oasis_Pipelines.Shared.Wpf.Converters;

public sealed class CollectionContainsConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values[0] is not IEnumerable<object> collection || values[1] is not { } item)
            return false;

        return collection.Contains(item);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}