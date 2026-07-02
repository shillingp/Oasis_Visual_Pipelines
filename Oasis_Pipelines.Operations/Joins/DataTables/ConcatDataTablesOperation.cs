using System.Data;
using Oasis_Pipelines.Operations.Attributes;
using Oasis_Pipelines.Operations.Classes;
using Oasis_Pipelines.Operations.Enums;
using Oasis_Pipelines.Operations.Functions;

namespace Oasis_Pipelines.Operations.Joins.DataTables;

[BlockOperationGroup(BlockOperationType.DataTable, BlockOperationGrouping.Join)]
public class ConcatDataTablesOperation : BlockOperation
{
    public override string OperationTitle => "Concat Tables";

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        BlockOperationResult? leftDataTableInput = inputOperations.FirstOrDefault(operation => operation.Result() is DataTable);
        BlockOperationResult? rightDataTableInput = inputOperations.FirstOrDefault(operation => operation != leftDataTableInput);

        if (leftDataTableInput?.Result() is not DataTable leftDataTable)
            return BlockOperationResult.NullOperation;

        return new BlockOperationResult(additionalOperations =>
        {
            if (rightDataTableInput?.Result() is not DataTable rightDataTable)
                return leftDataTable;

            string[] leftColumns = DataTableFunctions.ExtractColumnNamesFromTable(leftDataTable);
            string[] rightColumns = DataTableFunctions.ExtractColumnNamesFromTable(rightDataTable);

            if (!leftColumns.Intersect(rightColumns).Any())
                return new FailedOperation("Tables contain no matching columns");

            return DataTableFunctions.ConcatDataTables(
                leftDataTable,
                rightDataTable);
        });
    }
}