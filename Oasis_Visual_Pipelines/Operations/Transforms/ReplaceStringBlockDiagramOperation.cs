using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Interfaces;
using PropertyChanged;

namespace Oasis_Visual_Pipelines.Operations
{
    [BlockOperationGroup(BlockOperationType.Text, BlockOperationGroup.Transforms)]
    public class ReplaceStringBlockDiagramOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => 1;
        public override string OperationTitle => "Replace String";

        public string SearchText { get; set; } = "";
        public string ReplaceText { get; set; } = "";

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            return new BlockOperationResult(additionalOperations =>
            {
                if (SearchText is null
                    || string.IsNullOrEmpty(SearchText)
                    || ReplaceText is null)
                    return null;

                if (inputOperations.Concat(additionalOperations)
                    .FirstOrDefault()?.Result() is not string inputText)
                    return null;

                return inputText.Replace(SearchText, ReplaceText);
            });
        }
    }
}
