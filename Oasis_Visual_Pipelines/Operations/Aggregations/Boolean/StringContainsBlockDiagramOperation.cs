using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Interfaces;

namespace Oasis_Visual_Pipelines.Operations
{
    [BlockOperationGroup(BlockOperationType.Boolean | BlockOperationType.Text, BlockOperationGroup.Aggregation)]
    public class StringContainsBlockDiagramOperation : IBlockDiagramOperation
    {
        public int MaxInputs => 2;
        public string OperationTitle => "String Contains";

        public BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
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
