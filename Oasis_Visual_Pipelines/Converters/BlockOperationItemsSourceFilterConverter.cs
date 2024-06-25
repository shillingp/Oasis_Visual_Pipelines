using Oasis_Visual_Pipelines.Controls;
using System.Collections;
using System.Globalization;
using System.Windows.Data;
using Oasis_Visual_Pipelines.Models;

namespace Oasis_Visual_Pipelines.Converters
{
    public  class BlockOperationItemsSourceFilterConverter : IMultiValueConverter
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
                .Where(blockControl => (blockControl.Block.Data.OperationTitle as string)?
                    .Contains(searchText, StringComparison.OrdinalIgnoreCase) ?? false);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
