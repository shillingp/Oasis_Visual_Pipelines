using System.Collections;
using System.Globalization;
using Oasis_Pipelines.Model;

namespace Oasis_Pipelines.Controls.Wpf.Converters;

public class AppendExtraConnectionConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values[0] is not ICollection<Connection> connections)
            return Enumerable.Empty<Connection>();

        // if (values[1] is int maximumNodes && connections.Count >= maximumNodes)
        //     return connections.ToArray();

        return connections.Append(null);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}