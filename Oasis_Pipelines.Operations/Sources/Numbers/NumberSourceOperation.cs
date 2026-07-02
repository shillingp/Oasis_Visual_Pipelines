using Oasis_Pipelines.Operations.Attributes;
using Oasis_Pipelines.Operations.Classes;
using Oasis_Pipelines.Operations.Enums;

namespace Oasis_Pipelines.Operations.Sources.Numbers;

[BlockOperationGroup(BlockOperationType.Number, BlockOperationGrouping.Sources)]
public sealed class NumberSourceOperation : BlockOperation
{
    public override string OperationTitle => "Number Source";

    public double NumberValue { get; set; }

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult(additionalOperations => NumberValue);
    }
}