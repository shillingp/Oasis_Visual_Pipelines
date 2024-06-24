using Oasis_Visual_Pipelines.Attributes;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace Oasis_Visual_Pipelines.Converters
{
    public  class BlockOperationGroupDataTypeConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null || value.GetType().GetCustomAttribute(typeof(BlockOperationGroupAttribute))
                is not BlockOperationGroupAttribute blockGroupAttribute)
                return null;

            return blockGroupAttribute.TypeGroup;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
