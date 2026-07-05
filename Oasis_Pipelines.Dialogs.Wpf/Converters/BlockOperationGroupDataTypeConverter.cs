using System.Globalization;
using System.Reflection;
using System.Windows.Data;
using Oasis_Pipelines.Operations.Attributes;

namespace Oasis_Pipelines.Dialogs.Wpf.Converters;

public class BlockOperationGroupDataTypeConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value?.GetType().GetCustomAttribute(typeof(BlockOperationGroupAttribute)) is not
            BlockOperationGroupAttribute blockGroupAttribute)
            return null;

        return blockGroupAttribute.TypeGroup;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}