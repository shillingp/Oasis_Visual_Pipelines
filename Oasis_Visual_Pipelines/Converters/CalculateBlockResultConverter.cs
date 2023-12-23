using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Interfaces;
using Oasis_Visual_Pipelines.Models;
using System.Globalization;
using System.Windows.Data;

namespace Oasis_Visual_Pipelines.Converters
{
    public class CalculateBlockResultConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not Block block || !block.GetType().IsGenericType
                || block.GetType().GetGenericArguments()[0].GetInterface(nameof(IBlockDiagramOperation)) is null)
                return string.Empty;

            return ((dynamic)block).CalculateFlowPathResult().Result();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
