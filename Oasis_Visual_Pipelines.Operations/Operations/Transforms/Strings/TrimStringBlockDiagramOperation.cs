﻿using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;

namespace Oasis_Visual_Pipelines.Operations.Transforms.Strings
{
    [BlockOperationGroup(BlockOperationType.Text, BlockOperationGrouping.Transforms)]
    public sealed class TrimStringBlockDiagramOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => 1;
        public override string OperationTitle => "Trim String";

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            return new BlockOperationResult(additionalOperations =>
            {
                BlockOperationResult? firstOperationResult = inputOperations
                    .Concat(additionalOperations)
                    .FirstOrDefault();

                return firstOperationResult?.Result() is string text
                    ? text.Trim() : "";
            });
        }
    }
}
