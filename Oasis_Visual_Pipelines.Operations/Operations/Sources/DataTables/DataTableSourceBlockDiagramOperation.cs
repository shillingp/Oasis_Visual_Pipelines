using CommunityToolkit.Mvvm.Input;
using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Dialogs;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Functions;
using System.Data;
using System.Windows.Input;

namespace Oasis_Visual_Pipelines.Operations.Sources.DataTables
{
    [BlockOperationGroup(BlockOperationType.DataTable, BlockOperationGrouping.Sources)]
    public class DataTableSourceBlockDiagramOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => 0;
        public override string OperationTitle => "Table Source";

        public DataTable resultDataTable = new DataTable();

        public DataTableSourceBlockDiagramOperation()
        {
#if DEBUG
            resultDataTable.Columns.Add("Index", typeof(int));
            resultDataTable.Columns.Add("First Name", typeof(string));
            resultDataTable.Columns.Add("Last Name", typeof(string));

            resultDataTable.Rows.Add(1, "Peter", "Shilling");
            resultDataTable.Rows.Add(2, "John", "Smith");
            resultDataTable.Rows.Add(3, "Joe", "Bloggs");
#endif
        }

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            return new BlockOperationResult(resultDataTable);
        }

        public ICommand EditResultTableDataManuallyCommand => new RelayCommand(async () =>
        {
            await DialogHostFunctions.CreateAndShowDialog(
                content: new EditDataTableDialog(),
                dataContext: resultDataTable,
                closeOnClickAway: true);
        });
    }
}
