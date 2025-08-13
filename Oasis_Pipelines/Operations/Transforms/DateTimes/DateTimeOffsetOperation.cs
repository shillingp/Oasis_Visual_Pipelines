using Oasis_Pipelines.Classes;
using Oasis_Pipelines.Globals;

namespace Oasis_Pipelines.Operations.Transforms.DateTimes;


internal class DateTimeOffsetOperation : BlockOperation
{
    public override string OperationTitle => "Offset DateTime";

    private int OffsetValue { get; set; } = 0;
    private TimeDateOffset TimeDateOffset { get; set; } = TimeDateOffset.Day;

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult(additionalOperations =>
        {
            BlockOperationResult? firstOperation = additionalOperations
                .Concat(inputOperations)
                .FirstOrDefault(operation => operation.CalculateResult() is DateTime);

            if (firstOperation is null) return null;

            DateTime inputDateTime = firstOperation.Value.CalculateResult<DateTime>();

            return TimeDateOffset switch
            {
                TimeDateOffset.Second => inputDateTime.AddSeconds(OffsetValue),
                TimeDateOffset.Minute => inputDateTime.AddMinutes(OffsetValue),
                TimeDateOffset.Hour => inputDateTime.AddHours(OffsetValue),
                TimeDateOffset.Day => inputDateTime.AddDays(OffsetValue),
                TimeDateOffset.Week => inputDateTime.AddDays(OffsetValue * 7),
                TimeDateOffset.Month => inputDateTime.AddMonths(OffsetValue),
                TimeDateOffset.Year => inputDateTime.AddYears(OffsetValue),
                _ => null,
            };
        });
    }
}