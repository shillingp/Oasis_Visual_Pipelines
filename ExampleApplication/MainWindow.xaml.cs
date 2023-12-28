using Oasis_Visual_Pipelines.Classes;
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
            Block<InsertColumnBlockDiagramOperation> insertColumn = MainBlockDiagram.AddBlock<InsertColumnBlockDiagramOperation>(new Point(450, 200), null,
                new InsertColumnBlockDiagramOperation { ColumnName = "Date"});
            Block<DateSourceBlockDiagramOperation> dateTimeSource = MainBlockDiagram.AddBlock<DateSourceBlockDiagramOperation>(new Point(200, 350));
            Block<FilterDataTableBlockDiagramOperation> tableFilter = MainBlockDiagram.AddBlock<FilterDataTableBlockDiagramOperation>(new Point(750, 250), null,
                new FilterDataTableBlockDiagramOperation
                {
                    SelectedFilters = [new DataTableFilter
                    {
                        Column = new KeyValuePair<string, Type>("First Name", typeof(string)),
                        Filter = new FilterFunctor("Starts With", "LIKE 'J*'"),
                    }]
                });

            tableSource.ConnectTo(insertColumn);
            dateTimeSource.ConnectTo(insertColumn);
            insertColumn.ConnectTo(tableFilter);
        }

        private void PreviewRefreshButton_Click(object sender, RoutedEventArgs e)
        {
            ResultsPreviewControl.GetBindingExpression(DataContextProperty)?.UpdateTarget();
            ResultsPreviewControl.GetBindingExpression(ContentProperty)?.UpdateTarget();
        }
    }
}