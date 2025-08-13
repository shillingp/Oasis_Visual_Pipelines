using Oasis_Pipelines.Classes;

namespace Oasis_Pipelines.Operations.Aggregations.Numbers;

public sealed class DivideNumberOperation : BlockOperation
{
    public override string OperationTitle => "Divide Numbers";

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult((additionalOperations) =>
        {
            BlockOperationResult[] allOperations = additionalOperations
                .Concat(inputOperations)
                .ToArray();

            return allOperations
                .Skip(1)
                .Aggregate(
                    (double)allOperations.First().CalculateResult(),
                    (total, item) => total / item.CalculateResult<double>());
        });
    }
}