using Oasis_Pipelines.Classes;

namespace Oasis_Pipelines.Operations.Aggregations.Numbers;


public sealed class AddNumberOperation : BlockOperation
{
    public override string OperationTitle => "Add Numbers";

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult((additionalOperations) => inputOperations
            .Concat(additionalOperations)
            .Aggregate(0d, (total, item) => total + item.CalculateResult<double>()));
    }
}