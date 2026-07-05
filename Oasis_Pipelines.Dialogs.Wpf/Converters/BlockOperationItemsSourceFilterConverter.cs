using System.Collections;
using System.Globalization;
using System.Windows.Data;
using Oasis_Pipelines.Controls.Wpf;
using Oasis_Pipelines.Operations;

namespace Oasis_Pipelines.Dialogs.Wpf.Converters;

public class BlockOperationItemsSourceFilterConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values[0] is not IEnumerable<BlockOperation> collection || values[1] is not string searchText)
            return Enumerable.Empty<object>();

        if (string.IsNullOrEmpty(searchText))
            return collection;

        return collection.Where(blockControl =>
            blockControl.OperationTitle.Contains(searchText, StringComparison.OrdinalIgnoreCase));
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}