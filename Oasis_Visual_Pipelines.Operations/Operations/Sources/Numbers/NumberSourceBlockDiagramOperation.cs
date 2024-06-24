using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;

namespace Oasis_Visual_Pipelines.Operations.Sources.Numbers
{
    [BlockOperationGroup(BlockOperationType.Number, BlockOperationGrouping.Sources)]
    public class NumberSourceBlockDiagramOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => 0;
        public override string OperationTitle => "Number Source";

        public double NumberValue { get; set; }

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            return new BlockOperationResult(additionalOperations => NumberValue);
        }
    }
}
