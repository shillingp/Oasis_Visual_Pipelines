using System.Data;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Oasis_Pipelines.Operations.Classes;
using Oasis_Pipelines.Operations.Functions;

namespace Oasis_Pipelines.Operations.Sources.DataTables;


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
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            DefaultExt = ".xlsx",
            Filter = "Excel Documents (.xlsx)|*.xlsx"
        };

        if (openFileDialog.ShowDialog() != true)
            return;

        FetchedExcelTable = DataTableFunctions.ImportExcelToDataTable(openFileDialog.FileName);
    });
}