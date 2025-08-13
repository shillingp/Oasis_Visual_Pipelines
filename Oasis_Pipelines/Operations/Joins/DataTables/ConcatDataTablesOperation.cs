using System.Data;
using Oasis_Pipelines.Classes;
using Oasis_Pipelines.Functions;

namespace Oasis_Pipelines.Operations.Joins.DataTables;


internal class ConcatDataTablesOperation : BlockOperation
{

    public override string OperationTitle => "Concat Tables";

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        BlockOperationResult? leftDataTableInput = inputOperations.FirstOrDefault(operation => operation.CalculateResult() is DataTable);
        BlockOperationResult? rightDataTableInput = inputOperations.FirstOrDefault(operation => operation != leftDataTableInput);

        if (leftDataTableInput?.CalculateResult() is not DataTable leftDataTable)
            return BlockOperationResult.NullOperation;

        return new BlockOperationResult(additionalOperations =>
        {
            if (rightDataTableInput?.CalculateResult() is not DataTable rightDataTable)
                return leftDataTable;

            string[] leftColumns = DataTableFunctions.ExtractColumnNamesFromTable(leftDataTable);
            string[] rightColumns = DataTableFunctions.ExtractColumnNamesFromTable(rightDataTable);

            if (!leftColumns.Intersect(rightColumns).Any())
                return new FailedOperationException("Tables contain no matching columns");

            return DataTableFunctions.ConcatDataTables(
                leftDataTable,
                rightDataTable);
        });
    }
}