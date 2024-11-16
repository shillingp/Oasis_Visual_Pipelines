using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;
using System.Data;

namespace Oasis_Visual_Pipelines.Operations.Transforms.Arrays
{
    [BlockOperationGroup(BlockOperationType.Array, BlockOperationGrouping.Transforms)]
    public sealed class MapArrayBlockDiagramOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => 2;
        public override string OperationTitle => "Map Array";

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            return new BlockOperationResult(additionalOperations =>
            {
                BlockOperationResult? arrayInput = inputOperations.FirstOrDefault(operation => operation.Result() is Array);
                BlockOperationResult? updateFunctionInput = inputOperations.FirstOrDefault(operation => operation != arrayInput);

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
}
