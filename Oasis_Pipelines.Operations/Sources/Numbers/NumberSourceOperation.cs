using Oasis_Pipelines.Classes;

namespace Oasis_Pipelines.Operations.Sources.Numbers;


public sealed class NumberSourceOperation(double numberValue) : BlockOperation
{

    public override string OperationTitle => "Number Source";

    private double NumberValue { get; set; } = numberValue;

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult(additionalOperations => NumberValue);
    }
}