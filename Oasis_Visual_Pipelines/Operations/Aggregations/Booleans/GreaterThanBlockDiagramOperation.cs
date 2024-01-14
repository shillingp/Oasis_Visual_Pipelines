using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Functions;
using Oasis_Visual_Pipelines.Interfaces;

namespace Oasis_Visual_Pipelines.Operations.Aggregations.Booleans
{
    [BlockOperationGroup(BlockOperationType.Boolean, BlockOperationGroup.Aggregation)]
    internal class GreaterThanBlockDiagramOperation : IBlockDiagramOperation
    {
        public int MaxInputs => 2;
        public string OperationTitle => "Greater Than";

        public BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            return new BlockOperationResult(additionalOperations =>
            {
                IEnumerable<BlockOperationResult> numbers = additionalOperations
                    .Concat(inputOperations)
                    .Where(HelperFunctions.IsNumeric);

                BlockOperationResult? firstNumericResult = numbers.ElementAtOrDefault(0);
                if (firstNumericResult is null || HelperFunctions.ConvertNumeric(firstNumericResult.Result()) is not double firstNumber)
                    return BlockOperationResult.NullOperation;

                BlockOperationResult? secondNumericResult = numbers.ElementAtOrDefault(1);
                if (secondNumericResult is null || HelperFunctions.ConvertNumeric(secondNumericResult.Result()) is not double secondNumber)
                    return new BlockOperationResult(firstNumber);

                return firstNumber > secondNumber;
            });
        }
    }
}
