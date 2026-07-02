using Oasis_Pipelines.Operations.Attributes;
using Oasis_Pipelines.Operations.Classes;
using Oasis_Pipelines.Operations.Enums;

namespace Oasis_Pipelines.Operations.Aggregations.Numbers;

[BlockOperationGroup(BlockOperationType.Number, BlockOperationGrouping.Aggregation)]
public sealed class MultiplyNumberOperation : BlockOperation
{
    public override string OperationTitle => "Multiply Numbers";

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult((additionalOperations) => inputOperations
            .Concat(additionalOperations)
            .Aggregate(1d, (total, item) => total * item.Result()));
    }
}