using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Functions;
using System.Data;
using System.Windows.Input;

namespace Oasis_Visual_Pipelines.Operations.Sources.DataTables
{
    [BlockOperationGroup(BlockOperationType.DataTable, BlockOperationGrouping.Sources)]
    public sealed class ExcelDataTableSourceBlockDiagramOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => 0;
        public override string OperationTitle => "Excel Data Source";

        public DataTable? FetchedExcelTable { get; set; } = null;

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
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
}
