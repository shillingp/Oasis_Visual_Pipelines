using Oasis_Pipelines.Operations.Attributes;
using Oasis_Pipelines.Operations.Classes;
using Oasis_Pipelines.Operations.Enums;
using Oasis_Visual_Pipelines.Models;

namespace Oasis_Pipelines.Operations.Aggregations.Numbers;

[BlockOperationGroup(BlockOperationType.Number, BlockOperationGrouping.Aggregation)]
public sealed class AddNumberOperation : BlockOperation
{
    public override string OperationTitle => "Add Numbers";

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult((additionalOperations) => inputOperations
            .Concat(additionalOperations)
            .Aggregate(0d, (total, item) => total + item.Result()));
    }
}