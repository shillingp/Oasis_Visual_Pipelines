using System.Data;
using Oasis_Pipelines.Classes;
using Oasis_Pipelines.Functions;

namespace Oasis_Pipelines.Operations.Joins.DataTables;


public sealed class JoinDataTablesOperation : BlockOperation
{
    public override string OperationTitle => "Join Tables";

    public string[] LeftColumns { get; set; } = [];
    public string[] RightColumns { get; set; } = [];

    public string? SelectedLeftColumn { get; set; }
    public string? SelectedRightColumn { get; set; }

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        BlockOperationResult? leftDataTableInput = inputOperations.FirstOrDefault(operation => operation.CalculateResult() is DataTable);
        BlockOperationResult? rightDataTableInput = inputOperations.FirstOrDefault(operation => operation != leftDataTableInput);

        if (leftDataTableInput?.CalculateResult() is not DataTable leftDataTable)
            return BlockOperationResult.NullOperation;

        LeftColumns = DataTableFunctions.ExtractColumnNamesFromTable(leftDataTable);

        return new BlockOperationResult(additionalOperations =>
        {
            if (rightDataTableInput?.CalculateResult() is not DataTable rightDataTable)
                return leftDataTable;

            RightColumns = DataTableFunctions.ExtractColumnNamesFromTable(rightDataTable);

            if (SelectedLeftColumn is null || SelectedRightColumn is null)
                return BlockOperationResult.NullOperation;

            if (leftDataTable.Columns[SelectedLeftColumn]?.DataType != rightDataTable.Columns[SelectedRightColumn]?.DataType)
                return new FailedOperationException("Column data types must match!");

            return DataTableFunctions.JoinDataTables(
                leftDataTable,
                rightDataTable,
                SelectedLeftColumn,
                SelectedRightColumn);
        });
    }
}