using Oasis_Pipelines.Classes;

namespace Oasis_Pipelines.Operations.Selects.Arrays;

public sealed class TakeArrayElementsOperation : BlockOperation
{

    public override string OperationTitle => "Take 'N' Elements";

    public int ElementCount { get; set; } = 1;

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult(additionalOperations =>
        {
            BlockOperationResult? firstArray = inputOperations
                .Concat(additionalOperations)
                .FirstOrDefault(operation => operation.CalculateResult() is Array);

            if (firstArray?.CalculateResult() is not Array arrayItem) return null;

            return arrayItem
                .Cast<dynamic>()
                .Take(ElementCount)
                .ToArray();
        });
    }
}