using Oasis_Pipelines.Operations.Attributes;
using Oasis_Pipelines.Operations.Classes;
using Oasis_Pipelines.Operations.Enums;

namespace Oasis_Pipelines.Operations.Operations.Transforms.Strings;

[BlockOperationGroup(BlockOperationType.Text, BlockOperationGrouping.Transforms)]
public sealed class TrimStringOperation : BlockOperation
{
    public override string OperationTitle => "Trim String";

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult(additionalOperations =>
        {
            BlockOperationResult? firstOperationResult = inputOperations
                .Concat(additionalOperations)
                .FirstOrDefault();

            return firstOperationResult?.Result() is string text
                ? text.Trim()
                : "";
        });
    }
}