using Oasis_Pipelines.Classes;

namespace Oasis_Pipelines.Operations.Aggregations.Strings;


public sealed class CombineStringOperation : BlockOperation
{
    public override string OperationTitle => "Combine String";

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult((additionalOperations) => inputOperations
            .Concat(additionalOperations)
            .Aggregate("", (accumulated, input) => accumulated + input.CalculateResult()));
    }
}