using Oasis_Visual_Pipelines.Interfaces;
using Oasis_Visual_Pipelines.Models;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ExampleApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PreviewRefreshButton_Click(object sender, RoutedEventArgs e)
        {
            ResultsPreviewControl.GetBindingExpression(DataContextProperty)?.UpdateTarget();
            ResultsPreviewControl.GetBindingExpression(ContentProperty)?.UpdateTarget();
        }
    }

    public class CalculateBlockResultConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Block block && block.GetType().IsGenericType
                && block.GetType().GetGenericArguments()[0].GetInterface(nameof(IBlockDiagramOperation)) is Type)
            {
                return ((dynamic)block).CalculateFlowPathResult().Result();
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}