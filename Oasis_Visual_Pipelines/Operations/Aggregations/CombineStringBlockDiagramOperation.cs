using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Interfaces;
using PropertyChanged;

namespace Oasis_Visual_Pipelines.Operations
{
    [AddINotifyPropertyChangedInterface]
    [BlockOperationGroup(BlockOperationType.Text, BlockOperationGroup.Aggregation)]
    public class CombineStringBlockDiagramOperation : IBlockDiagramOperation
    {
        public int MaxInputs => int.MaxValue;
        public string OperationTitle => "Combine String";

        public BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            return new BlockOperationResult((additionalOperations) => inputOperations
                .Concat(additionalOperations)
                .Aggregate("", (accumulated, input) => accumulated + input.Result()));
        }
    }
}
