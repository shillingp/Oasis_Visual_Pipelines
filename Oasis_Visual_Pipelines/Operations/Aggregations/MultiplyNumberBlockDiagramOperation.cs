﻿using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Interfaces;
using PropertyChanged;

namespace Oasis_Visual_Pipelines.Operations
{
    [AddINotifyPropertyChangedInterface]
    [BlockOperationGroup(BlockOperationType.Number, BlockOperationGroup.Aggregation)]
    public class MultiplyNumberBlockDiagramOperation : IBlockDiagramOperation
    {
        public int MaxInputs => int.MaxValue;
        public string OperationTitle => "Multiply Numbers";

        public BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            return new BlockOperationResult((additionalOperations) => inputOperations
                .Concat(additionalOperations)
                .Aggregate(1d, (total, item) => total * item.Result()));
        }
    }
}
