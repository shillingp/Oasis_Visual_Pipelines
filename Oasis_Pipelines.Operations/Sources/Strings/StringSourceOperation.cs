using Oasis_Pipelines.Classes;

namespace Oasis_Pipelines.Operations.Sources.Strings;


public sealed class StringSourceOperation : BlockOperation
{
    public override string OperationTitle => "String Source";

    public string? TextValue { get; set; }

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult(additionalOperations => TextValue);
    }
}