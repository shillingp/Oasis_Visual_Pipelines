using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;

namespace Oasis_Visual_Pipelines.Operations
{
    [BlockOperationGroup(Enums.BlockOperationType.Number, Enums.BlockOperationGroup.Transforms)]
    public class DivideNumberBlockDiagramOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => int.MaxValue;
        public override string OperationTitle => "Divide Numbers";

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
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
