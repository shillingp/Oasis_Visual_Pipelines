using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Functions;
using Oasis_Visual_Pipelines.Interfaces;
using PropertyChanged;
using System.Data;

namespace Oasis_Visual_Pipelines.Operations
{
    [BlockOperationGroup(BlockOperationType.DataTable, BlockOperationGroup.Select)]
    public class SortDataTableBlockOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => 1;
        public override string OperationTitle => "Sort Table";

        public string[] ValidColumns { get; set; } = [];
        public string? ColumnName { get; set; }

        public SortDirection SortDirection { get; set; } = SortDirection.Ascending;

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            BlockOperationResult? dataTableInput = inputOperations.FirstOrDefault(operation => operation.Result() is DataTable);

            if (dataTableInput?.Result() is not DataTable dataTable)
            {
                ValidColumns = [];
                return BlockOperationResult.NullOperation;
            }

            ValidColumns = DataTableFunctions.ExtractColumnNamesFromTable(dataTable);

            return new BlockOperationResult(additionalOperations =>
            {
                DataTable inputTable = dataTable.Copy();

                if (ColumnName is null) return inputTable;

                string sortDirectionString = SortDirection == SortDirection.Ascending ? "asc" : "desc";
                inputTable.DefaultView.Sort = $"{ColumnName} {sortDirectionString}";

                return inputTable;
            });
        }
    }

    public enum SortDirection
    {
        Ascending,
        Descending
    }
}
