using Oasis_Visual_Pipelines.Operations.Attributes;
using Oasis_Visual_Pipelines.Operations.Classes;
using Oasis_Visual_Pipelines.Operations.Enums;

namespace Oasis_Visual_Pipelines.Operations.Sources.DateTimes
{
    [BlockOperationGroup(BlockOperationType.DateTime, BlockOperationGroup.Sources)]
    public class DateSourceBlockDiagramOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => 0;
        public override string OperationTitle => "Date Source";

        public DateTime SelectedDate { get; set; } = DateTime.Today;

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            return new BlockOperationResult(SelectedDate);
        }
    }
}
