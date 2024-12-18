﻿using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;

namespace Oasis_Visual_Pipelines.Operations.Sources.DateTimes
{
    [BlockOperationGroup(BlockOperationType.DateTime, BlockOperationGrouping.Sources)]
    public sealed class DateSourceBlockDiagramOperation : BaseBlockDiagramOperation
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
