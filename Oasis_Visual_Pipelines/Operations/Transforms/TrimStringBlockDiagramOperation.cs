using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Interfaces;
using PropertyChanged;

namespace Oasis_Visual_Pipelines.Operations
{
    [BlockOperationGroup(BlockOperationType.Text, BlockOperationGroup.Transforms)]
    public class TrimStringBlockDiagramOperation : BaseBlockDiagramOperation
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
