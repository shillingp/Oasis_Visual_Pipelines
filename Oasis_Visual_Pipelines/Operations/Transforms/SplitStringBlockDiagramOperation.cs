using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Functions;
using Oasis_Visual_Pipelines.Interfaces;
using PropertyChanged;
using System.Text.RegularExpressions;

namespace Oasis_Visual_Pipelines.Operations
{
    [AddINotifyPropertyChangedInterface]
    [BlockOperationGroup(BlockOperationType.Text, BlockOperationGroup.Transforms)]
    public class SplitStringBlockDiagramOperation : IBlockDiagramOperation
    {
        public int MaxInputs => 1;
        public string OperationTitle => "Split String";

        public string? SplitString { get; set; }

        public BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
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
