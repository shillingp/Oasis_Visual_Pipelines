using Oasis_Pipelines.Operations.Attributes;
using Oasis_Pipelines.Operations.Classes;
using Oasis_Pipelines.Operations.Enums;

namespace Oasis_Pipelines.Operations.Transforms.Arrays;

[BlockOperationGroup(BlockOperationType.Array, BlockOperationGrouping.Transforms)]
public sealed class MapArrayOperation : BlockOperation
{
    public override string OperationTitle => "Map Array";

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult(additionalOperations =>
        {
            BlockOperationResult? arrayInput = inputOperations.FirstOrDefault(operation => operation.Result() is Array);
            BlockOperationResult? updateFunctionInput =
                inputOperations.FirstOrDefault(operation => operation != arrayInput);

            if (arrayInput?.Result() is not Array resultantArray)
                return null;

            if (updateFunctionInput is null)
                return resultantArray;

            return resultantArray
                .Cast<dynamic>()
                .Select(arrayItem => updateFunctionInput.Result(
                    new BlockOperationResult(arrayItem)))
                .ToArray();
        });
    }
}