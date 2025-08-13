using System.Data;
using Oasis_Pipelines.Classes;
using Oasis_Pipelines.Functions;

namespace Oasis_Pipelines.Operations.Selects.DataTables;


public sealed class SortDataTableOperation : BlockOperation
{
    public override string OperationTitle => "Sort Table";

    public string[] ValidColumns { get; set; } = [];
    public string? ColumnName { get; set; }

    public SortDirection SortDirection { get; set; } = SortDirection.Ascending;

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        BlockOperationResult? dataTableInput = inputOperations.FirstOrDefault(operation => operation.CalculateResult() is DataTable);

        if (dataTableInput?.CalculateResult() is not DataTable dataTable)
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