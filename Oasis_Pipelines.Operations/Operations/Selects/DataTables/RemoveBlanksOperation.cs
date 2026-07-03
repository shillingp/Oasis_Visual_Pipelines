using System.Data;
using Oasis_Pipelines.Operations.Attributes;
using Oasis_Pipelines.Operations.Classes;
using Oasis_Pipelines.Operations.Enums;

namespace Oasis_Pipelines.Operations.Operations.Selects.DataTables;

[BlockOperationGroup(BlockOperationType.DataTable, BlockOperationGrouping.Select)]
public sealed class RemoveBlanksOperation : BlockOperation
{
    public override string OperationTitle => "Remove Blanks";

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        BlockOperationResult? tableOperation = inputOperations
            .FirstOrDefault(operation => operation?.Result() is DataTable);

        if (tableOperation?.Result() is not DataTable inputTable)
            return BlockOperationResult.NullOperation;

        return new BlockOperationResult(additionalOperations => inputTable.Rows
            .Cast<DataRow>()
            .Where(row => !row.ItemArray.All(field => field is DBNull || string.IsNullOrEmpty(field as string)))
            .CopyToDataTable());
    }
}