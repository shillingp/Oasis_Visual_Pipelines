using System.Data;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Oasis_Pipelines.Operations.Attributes;
using Oasis_Pipelines.Operations.Classes;
using Oasis_Pipelines.Operations.Enums;

namespace Oasis_Pipelines.Operations.Operations.Sources.DataTables;

[BlockOperationGroup(BlockOperationType.DataTable, BlockOperationGrouping.Sources)]
public sealed class ExcelDataTableSourceOperation : BlockOperation
{
    public override string OperationTitle => "Excel Data Source";

    public DataTable? FetchedExcelTable { get; set; } = null;

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        if (FetchedExcelTable is not null)
            return new BlockOperationResult(FetchedExcelTable);

        return BlockOperationResult.NullOperation;
    }

    public ICommand UpdateExcelSourceCommand => new RelayCommand(() =>
    {
        // SelectedFile? selectedFile = SelectFileDialog(
        //     DefaultExtension: ".xlsx",
        //     Filter: "Excel Documents (.xlsx)|*.xlsx");
        //
        // if (selectedFile is null)
        //     return;
        //
        // FetchedExcelTable = DataTableFunctions.ImportExcelToDataTable(selectedFile);
    });
}