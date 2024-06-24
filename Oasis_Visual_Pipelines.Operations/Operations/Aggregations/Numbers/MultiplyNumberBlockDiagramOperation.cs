using Oasis_Visual_Pipelines.Operations.Attributes;
using Oasis_Visual_Pipelines.Operations.Classes;
using Oasis_Visual_Pipelines.Operations.Enums;

namespace Oasis_Visual_Pipelines.Operations.Aggregations.Numbers
{
    [BlockOperationGroup(BlockOperationType.Number, BlockOperationGroup.Aggregation)]
    public class MultiplyNumberBlockDiagramOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => int.MaxValue;
        public override string OperationTitle => "Multiply Numbers";

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            return new BlockOperationResult((additionalOperations) => inputOperations
                .Concat(additionalOperations)
                .Aggregate(1d, (total, item) => total * item.Result()));
        }
    }
}
