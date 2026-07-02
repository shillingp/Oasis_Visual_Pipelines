using Oasis_Pipelines.Operations.Attributes;
using Oasis_Pipelines.Operations.Classes;
using Oasis_Pipelines.Operations.Enums;
using Oasis_Visual_Pipelines.Models;

namespace Oasis_Pipelines.Operations.Aggregations.Booleans;

[BlockOperationGroup(BlockOperationType.Boolean, BlockOperationGrouping.Aggregation)]
public class EqualToOperation : BlockOperation
{
    public override string OperationTitle => "Equal To";

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult(additionalOperations =>
        {
            BlockOperationResult[] numbers = [.. additionalOperations, .. inputOperations];

            if (numbers.Length < 2)
                return BlockOperationResult.NullOperation;

            return numbers[0].Result() == numbers[1].Result();
        });
    }
}