using Oasis_Pipelines.Operations.Attributes;
using Oasis_Pipelines.Operations.Classes;
using Oasis_Pipelines.Operations.Enums;

namespace Oasis_Pipelines.Operations.Operations.Sources.Strings;

[BlockOperationGroup(BlockOperationType.Text, BlockOperationGrouping.Sources)]
public sealed class StringSourceOperation : BlockOperation
{
    public override string OperationTitle => "String Source";

    public string? TextValue { get; set; }

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult(additionalOperations => TextValue);
    }
}