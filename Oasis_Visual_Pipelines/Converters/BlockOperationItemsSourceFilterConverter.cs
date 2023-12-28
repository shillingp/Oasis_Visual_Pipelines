using Oasis_Visual_Pipelines.Controls;
using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace Oasis_Visual_Pipelines.Converters
{
    internal class BlockOperationItemsSourceFilterConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is not IEnumerable collection || values[1] is not string searchText)
                return Enumerable.Empty<object>();

            if (string.IsNullOrEmpty(searchText))
                return collection;

            return collection
                .OfType<BlockControl>()
                .Cast<dynamic>()
                .Where(blockControl => ((string)blockControl.Block.Data.OperationTitle)
                    .Contains(searchText, StringComparison.OrdinalIgnoreCase));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
