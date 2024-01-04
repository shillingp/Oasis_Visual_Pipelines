using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Interfaces;
using PropertyChanged;

namespace Oasis_Visual_Pipelines.Operations
{
    [BlockOperationGroup(BlockOperationType.DateTime, BlockOperationGroup.Transforms)]
    internal class DateTimeOffsetBlockDiagramOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => 1;
        public override string OperationTitle => "Offset DateTime";

        public int OffsetValue { get; set; } = 0;
        public TimeDateOffset TimeDateOffset { get; set; } = TimeDateOffset.Day;

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            return new BlockOperationResult(additionalOperations =>
            {
                BlockOperationResult? firstOperation = additionalOperations
                    .Concat(inputOperations)
                    .FirstOrDefault(operation => operation.Result() is DateTime);

                if (firstOperation is null) return null;

                DateTime inputDateTime = firstOperation.Result();

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
}
