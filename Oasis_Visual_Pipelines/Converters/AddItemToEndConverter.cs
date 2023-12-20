using Oasis_Visual_Pipelines.Models;
using System.Globalization;
using System.Windows.Data;

namespace Oasis_Visual_Pipelines.Controls
{
    public class AddItemToEndConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is not ICollection<Connection> connections)
                return Enumerable.Empty<Connection>();

            if (values[1] is int maximumNodes && connections.Count >= maximumNodes)
                return connections;

            return connections.Append(null);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
