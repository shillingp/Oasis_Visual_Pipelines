﻿using Oasis_Visual_Pipelines.Classes;

namespace Oasis_Visual_Pipelines.Operations
{
    public class SubtractNumberBlockDiagramOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => int.MaxValue;
        public override string OperationTitle => "Subtract Numbers";

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            return new BlockOperationResult(additionalOperations =>
            {
                IEnumerable<BlockOperationResult> allOperations = additionalOperations
                    .Concat(inputOperations);

                return allOperations
                    .Skip(1)
                    .Aggregate(
                        (double)allOperations.First().Result(),
                        (total, item) => total + item.Result());
            });
        }
    }
}