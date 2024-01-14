using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Interfaces;

namespace Oasis_Visual_Pipelines.Operations
{
    [BlockOperationGroup(BlockOperationType.Boolean, BlockOperationGroup.Aggregation)]
    internal class EqualToBlockDiagramOperation : IBlockDiagramOperation
    {
        public int MaxInputs => 2;
        public string OperationTitle => "Equal To";

        public BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            return new BlockOperationResult(additionalOperations =>
            {
                BlockOperationResult[] numbers = [..additionalOperations, ..inputOperations];

                if (numbers.Length < 2)
                    return BlockOperationResult.NullOperation;

                return numbers[0].Result() == numbers[1].Result();
            });
        }
    }
}
