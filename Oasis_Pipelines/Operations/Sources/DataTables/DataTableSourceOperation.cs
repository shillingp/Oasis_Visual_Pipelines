using System.Data;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Oasis_Pipelines.Classes;

namespace Oasis_Pipelines.Operations.Sources.DataTables;


public sealed class DataTableSourceOperation : BlockOperation
{

    public override string OperationTitle => "Table Source";

    public DataTable resultDataTable = new DataTable();

    public DataTableSourceOperation()
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