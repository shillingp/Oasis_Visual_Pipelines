using Oasis_Pipelines.Operations.Attributes;
using Oasis_Pipelines.Operations.Classes;
using Oasis_Pipelines.Operations.Enums;

namespace Oasis_Pipelines.Operations.Operations.Aggregations.Strings;

[BlockOperationGroup(BlockOperationType.Text, BlockOperationGrouping.Aggregation)]
public sealed class CombineStringOperation : BlockOperation
{
    public override string OperationTitle => "Combine String";

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult((additionalOperations) => inputOperations
            .Concat(additionalOperations)
            .Aggregate("", (accumulated, input) => accumulated + input.Result()));
    }
}