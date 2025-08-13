using Oasis_Pipelines.Classes;

namespace Oasis_Pipelines.Operations.Aggregations.Booleans;


internal class EqualToOperation : BlockOperation
{

    public override string OperationTitle => "Equal To";

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult(additionalOperations =>
        {
            BlockOperationResult[] numbers = [.. additionalOperations, .. inputOperations];

            if (numbers.Length < 2)
                return BlockOperationResult.NullOperation;

            return numbers[0].CalculateResult() == numbers[1].CalculateResult();
        });
    }
}