using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Interfaces;
using PropertyChanged;

namespace Oasis_Visual_Pipelines.Operations
{
    [AddINotifyPropertyChangedInterface]
    [BlockOperationGroup(BlockOperationType.Text, BlockOperationGroup.Transforms)]
    public class TrimStringBlockDiagramOperation : IBlockDiagramOperation
    {
        public int MaxInputs => 1;
        public string OperationTitle => "Trim String";

        public BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            return new BlockOperationResult(additionalOperations =>
                inputOperations[0].Result() is string text ? text.Trim() : "");
        }
    }
}
