using Oasis_Pipelines.Operations.Attributes;
using Oasis_Pipelines.Operations.Classes;

namespace Oasis_Pipelines.Operations.Aggregations.Numbers;

[BlockOperationGroup(Enums.BlockOperationType.Number, Enums.BlockOperationGrouping.Transforms)]
public sealed class DivideNumberOperation : BlockOperation
{
    public override string OperationTitle => "Divide Numbers";

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult((additionalOperations) =>
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