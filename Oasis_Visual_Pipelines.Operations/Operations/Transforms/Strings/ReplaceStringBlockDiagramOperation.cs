using Oasis_Visual_Pipelines.Operations.Attributes;
using Oasis_Visual_Pipelines.Operations.Classes;
using Oasis_Visual_Pipelines.Operations.Enums;

namespace Oasis_Visual_Pipelines.Operations.Transforms.Strings
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
