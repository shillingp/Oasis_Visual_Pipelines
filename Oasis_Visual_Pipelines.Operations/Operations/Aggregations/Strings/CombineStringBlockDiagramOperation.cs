using Oasis_Visual_Pipelines.Operations.Attributes;
using Oasis_Visual_Pipelines.Operations.Classes;
using Oasis_Visual_Pipelines.Operations.Enums;

namespace Oasis_Visual_Pipelines.Operations.Aggregations.Strings
{
    [BlockOperationGroup(BlockOperationType.Text, BlockOperationGroup.Aggregation)]
    public class CombineStringBlockDiagramOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => int.MaxValue;
        public override string OperationTitle => "Combine String";

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            return new BlockOperationResult((additionalOperations) => inputOperations
                .Concat(additionalOperations)
                .Aggregate("", (accumulated, input) => accumulated + input.Result()));
        }
    }
}
