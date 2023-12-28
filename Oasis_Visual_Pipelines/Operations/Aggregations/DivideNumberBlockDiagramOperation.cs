using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Interfaces;
using PropertyChanged;

namespace Oasis_Visual_Pipelines.Operations
{
    [AddINotifyPropertyChangedInterface]
    [BlockOperationGroup(Enums.BlockOperationType.Number, Enums.BlockOperationGroup.Transforms)]
    internal class DivideNumberBlockDiagramOperation : IBlockDiagramOperation
    {
        public int MaxInputs => int.MaxValue;

        public string OperationTitle => "Divide Numbers";

        public BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            return new BlockOperationResult((additionalOperations) =>
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
