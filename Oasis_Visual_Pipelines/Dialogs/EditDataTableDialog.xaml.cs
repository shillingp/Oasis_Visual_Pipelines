using CommunityToolkit.Mvvm.Input;
using System.Data;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace Oasis_Visual_Pipelines.Dialogs
{
    /// <summary>
    /// Interaction logic for EditDataTableDialog.xaml
    /// </summary>
    public partial class EditDataTableDialog : UserControl
    {
        public EditDataTableDialog()
        {
            InitializeComponent();
        }

        private void DataGrid_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataGrid editDataGrid = ((DataGrid)sender);
            MoveColumnAdditionColumnToEnd(editDataGrid);
        }

        public static ICommand AddNewColumnCommand => new RelayCommand<DataGrid>((dataGrid) =>
        {
            DataTable editingTable = (DataTable)dataGrid!.DataContext;
            editingTable.Columns.Add(new DataColumn());

            dataGrid.AutoGenerateColumns = false;
            dataGrid.AutoGenerateColumns = true;

            MoveColumnAdditionColumnToEnd(dataGrid);
        });

        private static void MoveColumnAdditionColumnToEnd(DataGrid editDataGrid)
        {
            DataGridTemplateColumn addNewColumn = editDataGrid.Columns
                .OfType<DataGridTemplateColumn>()
                .First();

            addNewColumn.DisplayIndex = editDataGrid.Columns.Count - 1;
        }
    }
}
