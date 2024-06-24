using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;

namespace Oasis_Visual_Pipelines.Operations.Selects.Arrays
{
    [BlockOperationGroup(Enums.BlockOperationType.Array, Enums.BlockOperationGrouping.Select)]
    public class TakeArrayElementsBlockDiagramOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => 1;
        public override string OperationTitle => "Take 'N' Elements";

        public int ElementCount { get; set; } = 1;

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
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
}
