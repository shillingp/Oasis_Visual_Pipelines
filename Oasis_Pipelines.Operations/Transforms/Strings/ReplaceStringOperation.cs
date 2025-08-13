using Oasis_Pipelines.Classes;

namespace Oasis_Pipelines.Operations.Transforms.Strings;


public sealed class ReplaceStringOperation : BlockOperation
{
    public override string OperationTitle => "Replace String";

    public string SearchText { get; set; } = "";
    public string ReplaceText { get; set; } = "";

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult(additionalOperations =>
        {
            if (SearchText is null
                || string.IsNullOrEmpty(SearchText)
                || ReplaceText is null)
                return null;

            if (inputOperations.Concat(additionalOperations)
                    .FirstOrDefault().CalculateResult() is not string inputText)
                return null;

            return inputText.Replace(SearchText, ReplaceText);
        });
    }
}