using Oasis_Pipelines.Operations.Attributes;
using Oasis_Pipelines.Operations.Classes;
using Oasis_Pipelines.Operations.Enums;

namespace Oasis_Pipelines.Operations.Aggregations.Numbers;

[BlockOperationGroup(BlockOperationType.Number, BlockOperationGrouping.Aggregation)]
public sealed class SubtractNumberOperation : BlockOperation
{
    public override string OperationTitle => "Subtract Numbers";

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult(additionalOperations =>
        {
            IEnumerable<BlockOperationResult> allOperations = additionalOperations
                .Concat(inputOperations);

            return allOperations
                .Skip(1)
                .Aggregate(
                    (double)allOperations.First().Result(),
                    (total, item) => total + item.Result());
        });
    }
}