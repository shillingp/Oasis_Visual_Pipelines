using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Interfaces;
using PropertyChanged;

namespace Oasis_Visual_Pipelines.Operations
{
    [AddINotifyPropertyChangedInterface]
    [BlockOperationGroup(BlockOperationType.Number, BlockOperationGroup.Aggregation)]
    public class AddNumberBlockDiagramOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => int.MaxValue;
        public override string OperationTitle => "Add Numbers";

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            return new BlockOperationResult((additionalOperations) => inputOperations
                .Concat(additionalOperations)
                .Aggregate(0d, (total, item) => total + item.Result()));
        }
    }
}
