using System.Globalization;
using System.Windows.Data;
using Oasis_Pipelines.Interfaces;
using Oasis_Pipelines.Model;

namespace Oasis_Pipelines.Controls.Converters;

[ValueConversion(typeof(object[]), typeof(IPipelineObject))]
public class ConcatenatePipelineObjectsConverter : IMultiValueConverter
{
    /// <inheritdoc />
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values is not [IEnumerable<IPipelineObject> blocks, IEnumerable<IPipelineObject> connections])
            return Array.Empty<IPipelineObject>();

        return blocks.Concat(connections);
        // return blocks
        //     .OfType<IPipelineObject>()
        //     .Concat(connections);
    }

    /// <inheritdoc />
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}