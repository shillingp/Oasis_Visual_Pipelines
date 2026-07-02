using Oasis_Pipelines.Operations.Attributes;
using Oasis_Pipelines.Operations.Classes;
using Oasis_Pipelines.Operations.Enums;

namespace Oasis_Pipelines.Operations.Sources.DateTimes;

[BlockOperationGroup(BlockOperationType.DateTime, BlockOperationGrouping.Sources)]
public sealed class DateSourceOperation : BlockOperation
{
    public override string OperationTitle => "Date Source";

    public DateTime SelectedDate { get; set; } = DateTime.Today;

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult(SelectedDate);
    }
}