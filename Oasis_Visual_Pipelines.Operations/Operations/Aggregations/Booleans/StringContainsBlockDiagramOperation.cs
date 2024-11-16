using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;

namespace Oasis_Visual_Pipelines.Operations.Aggregations.Booleans
{
    [BlockOperationGroup(BlockOperationType.Boolean, BlockOperationGrouping.Aggregation)]
    public sealed class StringContainsBlockDiagramOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => 2;
        public override string OperationTitle => "String Contains";

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            return new BlockOperationResult(additionalOperations =>
            {
                IEnumerable<BlockOperationResult> allOperations = additionalOperations.Concat(inputOperations);

                BlockOperationResult? textInputOperation = allOperations.FirstOrDefault(operation => operation.Result() is string);
                if (textInputOperation?.Result() is not string inputText)
                    return BlockOperationResult.NullOperation;

                BlockOperationResult? searchTextOperation = allOperations.FirstOrDefault(operation => operation != textInputOperation);
                if (searchTextOperation?.Result() is not string searchText)
                    return inputText;

                return inputText.Contains(searchText, StringComparison.OrdinalIgnoreCase);
            });
        }
    }
}
