using System.Globalization;
using System.Windows.Data;
using Oasis_Pipelines.Interfaces;
using Oasis_Pipelines.Model;

namespace Oasis_Pipelines.Diagrams.Converters;

[ValueConversion(typeof(object[]), typeof(IPipelineObject))]
public class ConcatenateConverter : IMultiValueConverter
{
    /// <inheritdoc />
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values is not [ICollection<Block> blocks, ICollection<Connection> connections])
            return Array.Empty<IPipelineObject>();

        return blocks
            .OfType<IPipelineObject>()
            .Concat(connections);
    }

    /// <inheritdoc />
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}