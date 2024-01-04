using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Interfaces;
using PropertyChanged;

namespace Oasis_Visual_Pipelines.Operations
{
    [AddINotifyPropertyChangedInterface]
    [BlockOperationGroup(BlockOperationType.Text, BlockOperationGroup.Sources)]
    public class StringSourceBlockDiagramOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => 0;
        public override string OperationTitle => "String Source";

        public string? TextValue { get; set; }

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            return new BlockOperationResult(additionalOperations => TextValue);
        }
    }
}
