using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Interfaces;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oasis_Visual_Pipelines.Operations
{
    [AddINotifyPropertyChangedInterface]
    [BlockOperationGroup(BlockOperationType.Text, BlockOperationGroup.Transforms)]
    public class ReplaceStringBlockDiagramOperation : IBlockDiagramOperation
    {
        public int MaxInputs => 1;
        public string OperationTitle => "Replace String";

        public string SearchText { get; set; } = "";
        public string ReplaceText { get; set; } = "";

        public BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
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
