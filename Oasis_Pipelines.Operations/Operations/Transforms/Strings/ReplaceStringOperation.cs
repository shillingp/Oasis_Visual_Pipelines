using Oasis_Pipelines.Operations.Attributes;
using Oasis_Pipelines.Operations.Classes;
using Oasis_Pipelines.Operations.Enums;

namespace Oasis_Pipelines.Operations.Operations.Transforms.Strings;

[BlockOperationGroup(BlockOperationType.Text, BlockOperationGrouping.Transforms)]
public sealed class ReplaceStringOperation : BlockOperation
{
    public override string OperationTitle => "Replace String";

    public string SearchText { get; set; } = "";
    public string ReplaceText { get; set; } = "";

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult(additionalOperations =>
        {
            if (string.IsNullOrEmpty(SearchText))
                return null;

            if (inputOperations.Concat(additionalOperations).FirstOrDefault()?.Result() is not string inputText)
                return null;

            return inputText.Replace(SearchText, ReplaceText);
        });
    }
}