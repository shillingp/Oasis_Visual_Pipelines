using Oasis_Visual_Pipelines.Operations.Attributes;
using Oasis_Visual_Pipelines.Operations.Classes;
using Oasis_Visual_Pipelines.Operations.Enums;

namespace Oasis_Visual_Pipelines.Operations.Sources.Strings
{
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
