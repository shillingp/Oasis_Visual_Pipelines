using Oasis_Pipelines.Classes;

namespace Oasis_Pipelines.Operations.Aggregations.Booleans;


internal class GreaterThanOperation : BlockOperation
{

    public override string OperationTitle => "Greater Than";

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult(additionalOperations =>
        {
            BlockOperationResult[] numbers = additionalOperations
                .Concat(inputOperations)
                // .Where(HelperFunctions.IsNumeric)
                .ToArray();

            BlockOperationResult? firstNumericResult = numbers.ElementAtOrDefault(0);
            if (firstNumericResult is null || firstNumericResult.Value.CalculateResult() is not double firstNumber)
                return BlockOperationResult.NullOperation;

            BlockOperationResult? secondNumericResult = numbers.ElementAtOrDefault(1);
            if (secondNumericResult is null || secondNumericResult.Value.CalculateResult() is not double secondNumber)
                return new BlockOperationResult(firstNumber);

            return firstNumber > secondNumber;
        });
    }
}