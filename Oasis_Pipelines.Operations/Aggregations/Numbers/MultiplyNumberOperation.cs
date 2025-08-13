using Oasis_Pipelines.Classes;

namespace Oasis_Pipelines.Operations.Aggregations.Numbers;


public sealed class MultiplyNumberOperation : BlockOperation
{
    public override string OperationTitle => "Multiply Numbers";

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult((additionalOperations) => inputOperations
            .Concat(additionalOperations)
            .Aggregate(1d, (total, item) => total * item.CalculateResult<double>()));
    }
}