using Oasis_Pipelines.Classes;

namespace Oasis_Pipelines.Operations.Transforms.Arrays;

public sealed class MapArrayOperation : BlockOperation
{
    public override string OperationTitle => "Map Array";

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult(additionalOperations =>
        {
            BlockOperationResult? arrayInput = inputOperations.FirstOrDefault(operation => operation.CalculateResult() is Array);
            BlockOperationResult? updateFunctionInput = inputOperations.FirstOrDefault(operation => operation != arrayInput);

            if (arrayInput?.CalculateResult() is not Array resultantArray)
                return null;

            if (updateFunctionInput is null)
                return resultantArray;

            return resultantArray
                .Cast<dynamic>()
                .Select(arrayItem => updateFunctionInput.Value.CalculateResult(
                    new BlockOperationResult(arrayItem)))
                .ToArray();
        });
    }
}