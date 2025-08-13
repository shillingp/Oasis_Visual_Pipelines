using Oasis_Pipelines.Classes;

namespace Oasis_Pipelines.Operations.Transforms.Strings;


public sealed class TrimStringOperation : BlockOperation
{
    public override string OperationTitle => "Trim String";

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult(additionalOperations =>
        {
            BlockOperationResult? firstOperationResult = inputOperations
                .Concat(additionalOperations)
                .FirstOrDefault();

            return firstOperationResult?.CalculateResult() is string text
                ? text.Trim() : "";
        });
    }
}