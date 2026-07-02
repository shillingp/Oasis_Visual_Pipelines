using System.Data;
using Oasis_Pipelines.Operations.Attributes;
using Oasis_Pipelines.Operations.Classes;
using Oasis_Pipelines.Operations.Enums;
using Oasis_Pipelines.Operations.Functions;

namespace Oasis_Pipelines.Operations.Selects.DataTables;

[BlockOperationGroup(BlockOperationType.DataTable, BlockOperationGrouping.Sources)]
public sealed class DistinctDataOperation : BlockOperation
{
    public override string OperationTitle => "Distinct Rows";

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        BlockOperationResult? tableOperation = inputOperations
            .FirstOrDefault(operation => operation?.Result() is DataTable);

        if (tableOperation?.Result() is not DataTable inputTable)
            return BlockOperationResult.NullOperation;

        return new BlockOperationResult(additionalOperations =>
        {
            return inputTable.DefaultView.ToTable(true,
                DataTableFunctions.ExtractColumnNamesFromTable(inputTable));
        });
    }
}