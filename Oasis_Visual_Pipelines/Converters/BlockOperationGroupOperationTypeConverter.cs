using Oasis_Visual_Pipelines.Attributes;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace Oasis_Visual_Pipelines.Converters
{
    internal class BlockOperationGroupOperationTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BlockOperationGroupAttribute blockGroupAttribute = value.GetType()
                .GetCustomAttribute(typeof(BlockOperationGroupAttribute))
                as BlockOperationGroupAttribute;
            if (value is null) return null;

            return blockGroupAttribute.OperationGroup;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
