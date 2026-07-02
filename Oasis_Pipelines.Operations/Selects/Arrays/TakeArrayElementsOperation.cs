using Oasis_Pipelines.Operations.Attributes;
using Oasis_Pipelines.Operations.Classes;

namespace Oasis_Pipelines.Operations.Selects.Arrays;

[BlockOperationGroup(Enums.BlockOperationType.Array, Enums.BlockOperationGrouping.Select)]
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
                .FirstOrDefault(operation => operation.Result() is Array);

            if (firstArray?.Result() is not Array arrayItem) return null;

            return arrayItem
                .Cast<dynamic>()
                .Take(ElementCount)
                .ToArray();
        });
    }
}