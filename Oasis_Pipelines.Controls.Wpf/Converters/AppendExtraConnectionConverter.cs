using System.Collections;
using System.Globalization;
using System.Windows.Data;
using Oasis_Pipelines.Model;

namespace Oasis_Pipelines.Controls.Wpf.Converters;

public class AppendExtraConnectionConverter : IMultiValueConverter
{
    private readonly IList<Connection> _currentConnections = new List<Connection>();

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values[0] is not ICollection<Connection> connections)
            return Enumerable.Empty<Connection>();

        foreach (Connection input in _currentConnections.Except(connections).ToArray())
            _currentConnections.Remove(input);
        
        foreach (Connection input in connections.Except(_currentConnections).ToArray())
            _currentConnections.Add(input);
        
        return connections.Append(null);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}