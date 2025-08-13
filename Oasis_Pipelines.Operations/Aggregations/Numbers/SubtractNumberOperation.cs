using Oasis_Pipelines.Classes;

namespace Oasis_Pipelines.Operations.Aggregations.Numbers;


public sealed class SubtractNumberOperation : BlockOperation
{
    public override string OperationTitle => "Subtract Numbers";

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult(additionalOperations =>
        {
            BlockOperationResult[] allOperations = additionalOperations
                .Concat(inputOperations)
                .ToArray();

            return allOperations
                .Skip(1)
                .Aggregate(
                    allOperations.First().CalculateResult<double>(),
                    (total, item) => total - item.CalculateResult<double>());
        });
    }
}