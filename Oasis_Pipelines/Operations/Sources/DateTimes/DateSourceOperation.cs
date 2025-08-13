using Oasis_Pipelines.Classes;

namespace Oasis_Pipelines.Operations.Sources.DateTimes;


public sealed class DateSourceOperation : BlockOperation
{

    public override string OperationTitle => "Date Source";

    public DateTime SelectedDate { get; set; } = DateTime.Today;

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult(SelectedDate);
    }
}