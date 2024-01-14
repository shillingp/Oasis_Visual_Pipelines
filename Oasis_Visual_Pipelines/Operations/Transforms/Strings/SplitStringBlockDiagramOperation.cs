using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Functions;
using System.Text.RegularExpressions;

namespace Oasis_Visual_Pipelines.Operations.Transforms.Strings
{
    [BlockOperationGroup(BlockOperationType.Text, BlockOperationGroup.Transforms)]
    public class SplitStringBlockDiagramOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => 1;
        public override string OperationTitle => "Split String";

        public string? SplitString { get; set; }

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
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
}
