using System.Text.RegularExpressions;
using Oasis_Pipelines.Operations.Attributes;
using Oasis_Pipelines.Operations.Classes;
using Oasis_Pipelines.Operations.Enums;
using Oasis_Pipelines.Operations.Functions;

namespace Oasis_Pipelines.Operations.Operations.Transforms.Strings;

[BlockOperationGroup(BlockOperationType.Text, BlockOperationGrouping.Transforms)]
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
                    .FirstOrDefault()?.Result() is not string inputString)
                return null;

            if (HelperFunctions.IsValidRegex(inputString))
                return Regex.Split(inputString, SplitString);

            return inputString.Split(SplitString);
        });
    }
}