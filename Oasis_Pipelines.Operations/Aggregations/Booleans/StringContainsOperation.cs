using Oasis_Pipelines.Classes;

namespace Oasis_Pipelines.Operations.Aggregations.Booleans;


public sealed class StringContainsOperation : BlockOperation
{

    public override string OperationTitle => "String Contains";

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult(additionalOperations =>
        {
            IEnumerable<BlockOperationResult> allOperations = additionalOperations.Concat(inputOperations);

            BlockOperationResult? textInputOperation = allOperations.FirstOrDefault(operation => operation.CalculateResult() is string);
            if (textInputOperation?.CalculateResult() is not string inputText)
                return BlockOperationResult.NullOperation;

            BlockOperationResult? searchTextOperation = allOperations.FirstOrDefault(operation => operation != textInputOperation);
            if (searchTextOperation?.CalculateResult() is not string searchText)
                return inputText;

            return inputText.Contains(searchText, StringComparison.OrdinalIgnoreCase);
        });
    }
}