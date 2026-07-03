using Oasis_Pipelines.Operations.Attributes;
using Oasis_Pipelines.Operations.Classes;
using Oasis_Pipelines.Operations.Enums;

namespace Oasis_Pipelines.Operations.Operations.Aggregations.Booleans;

[BlockOperationGroup(BlockOperationType.Boolean, BlockOperationGrouping.Aggregation)]
public sealed class StringContainsOperation : BlockOperation
{
    public override string OperationTitle => "String Contains";

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult(additionalOperations =>
        {
            IEnumerable<BlockOperationResult> allOperations = additionalOperations.Concat(inputOperations);

            BlockOperationResult? textInputOperation =
                allOperations.FirstOrDefault(operation => operation.Result() is string);
            if (textInputOperation?.Result() is not string inputText)
                return BlockOperationResult.NullOperation;

            BlockOperationResult? searchTextOperation =
                allOperations.FirstOrDefault(operation => operation != textInputOperation);
            if (searchTextOperation?.Result() is not string searchText)
                return inputText;

            return inputText.Contains(searchText, StringComparison.OrdinalIgnoreCase);
        });
    }
}