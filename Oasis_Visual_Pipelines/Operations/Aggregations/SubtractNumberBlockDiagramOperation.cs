using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Interfaces;

namespace Oasis_Visual_Pipelines.Operations
{
    public class SubtractNumberBlockDiagramOperation : IBlockDiagramOperation
    {
        public int MaxInputs => int.MaxValue;
        public string OperationTitle => "Subtract Numbers";

        public BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
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