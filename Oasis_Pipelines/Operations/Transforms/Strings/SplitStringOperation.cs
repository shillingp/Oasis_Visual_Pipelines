using System.Text.RegularExpressions;
using Oasis_Pipelines.Classes;

namespace Oasis_Pipelines.Operations.Transforms.Strings;


public sealed class SplitStringOperation : BlockOperation
{
    public override string OperationTitle => "Split String";

    public string? SplitString { get; set; }

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult(additionalOperations =>
        {
            if (SplitString is null)
                return null;

            if (inputOperations.Concat(additionalOperations)
                    .FirstOrDefault().CalculateResult() is not string inputString)
                return null;

            return inputString.Split(SplitString);
        });
    }
}