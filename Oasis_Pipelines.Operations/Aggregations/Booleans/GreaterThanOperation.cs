using Oasis_Pipelines.Operations.Attributes;
using Oasis_Pipelines.Operations.Classes;
using Oasis_Pipelines.Operations.Enums;
using Oasis_Pipelines.Operations.Functions;

namespace Oasis_Pipelines.Operations.Aggregations.Booleans;

[BlockOperationGroup(BlockOperationType.Boolean, BlockOperationGrouping.Aggregation)]
public class GreaterThanOperation : BlockOperation
{
    public override string OperationTitle => "Greater Than";

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult(additionalOperations =>
        {
            IEnumerable<BlockOperationResult> numbers = additionalOperations
                .Concat(inputOperations)
                .Where(HelperFunctions.IsNumeric);

            BlockOperationResult? firstNumericResult = numbers.ElementAtOrDefault(0);
            if (firstNumericResult is null ||
                HelperFunctions.ConvertNumeric(firstNumericResult.Result()) is not double firstNumber)
                return BlockOperationResult.NullOperation;

            BlockOperationResult? secondNumericResult = numbers.ElementAtOrDefault(1);
            if (secondNumericResult is null ||
                HelperFunctions.ConvertNumeric(secondNumericResult.Result()) is not double secondNumber)
                return new BlockOperationResult(firstNumber);

            return firstNumber > secondNumber;
        });
    }
}