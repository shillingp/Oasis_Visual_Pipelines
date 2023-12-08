using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Interfaces;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oasis_Visual_Pipelines.Operations
{
    [AddINotifyPropertyChangedInterface]
    [BlockOperationGroup(BlockOperationType.Array, BlockOperationGroup.Transforms)]
    public class MapArrayBlockDiagramOperation : IBlockDiagramOperation
    {
        public int MaxInputs => 2;
        public string OperationTitle => "Map Array";

        public BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            return new BlockOperationResult(additionalOperations =>
            {
                BlockOperationResult? arrayInput = inputOperations.FirstOrDefault(operation => operation.Result() is Array);
                BlockOperationResult? updateFunctionInput = inputOperations.FirstOrDefault(operation => operation != arrayInput);

                if (arrayInput?.Result() is not Array resultantArray)
                    return BlockOperationResult.NullOperation;

                if (updateFunctionInput is null)
                    return resultantArray;

                return resultantArray
                    .Cast<dynamic>()
                    .Select(arrayItem => updateFunctionInput.Result(
                        new BlockOperationResult(arrayItem)))
                    .ToArray();
            });
        }
    }
}
