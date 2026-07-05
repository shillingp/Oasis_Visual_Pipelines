using Oasis_Pipelines.Operations.Attributes;
using Oasis_Pipelines.Operations.Classes;
using Oasis_Pipelines.Operations.Enums;

namespace Oasis_Pipelines.Operations.Operations.Aggregations.Numbers;

[BlockOperationGroup(BlockOperationType.Number, BlockOperationGrouping.Transforms)]
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
                    allOperations[0].Result() is double seed ? seed : 0,
                    (total, item) => total + item.Result());
        });
    }
}