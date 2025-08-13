using System.Data;
using Oasis_Pipelines.Classes;
using Oasis_Pipelines.Functions;

namespace Oasis_Pipelines.Operations.Transforms.DataTables;

public sealed class RenameColumnOperation : BlockOperation
{
    public override string OperationTitle => "Rename Column";

    public string[]? ValidColumns { get; set; } = null;

    public string? SelectedColumn { get; set; }
    public string? NewColumnName { get; set; }

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        BlockOperationResult? leftDataTableInput = inputOperations.FirstOrDefault(operation => operation.CalculateResult() is DataTable);

        if (leftDataTableInput?.CalculateResult() is not DataTable inputDataTable)
        {
            ValidColumns = [];
            return BlockOperationResult.NullOperation;
        }

        ValidColumns = DataTableFunctions.ExtractColumnNamesFromTable(inputDataTable);

        return new BlockOperationResult(additionalOperations =>
        {
            if (SelectedColumn is null || NewColumnName is null || inputDataTable.Columns[SelectedColumn] is null)
                return inputDataTable;

            inputDataTable.Columns[SelectedColumn]!.ColumnName = NewColumnName;

            return inputDataTable;
        });
    }
}