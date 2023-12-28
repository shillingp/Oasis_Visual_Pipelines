using Oasis_Visual_Pipelines.Models;
using Oasis_Visual_Pipelines.Operations;
using System.Windows;

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

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Block<DataTableSourceBlockDiagramOperation> tableSource = MainBlockDiagram.AddBlock<DataTableSourceBlockDiagramOperation>(new Point(150, 150));
            Block<FilterDataTableBlockDiagramOperation> tableFilter = MainBlockDiagram.AddBlock<FilterDataTableBlockDiagramOperation>(new Point(450, 300));

            tableSource.ConnectTo(tableFilter);
        }

        private void PreviewRefreshButton_Click(object sender, RoutedEventArgs e)
        {
            ResultsPreviewControl.GetBindingExpression(DataContextProperty)?.UpdateTarget();
            ResultsPreviewControl.GetBindingExpression(ContentProperty)?.UpdateTarget();
        }
    }
}