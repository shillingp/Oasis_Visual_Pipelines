using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Interfaces;

namespace Oasis_Visual_Pipelines.Operations
{
    [BlockOperationGroup(BlockOperationType.DataTable, BlockOperationGroup.Transforms)]
    public class InsertColumnBlockDiagramOperation : IBlockDiagramOperation
    {
        public int MaxInputs => 2;

        public string OperationTitle => "Insert Column";

        public string ColumnName { get; set; }

        public BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            return new BlockOperationResult(additionalOperations =>
            {
                //if (inputOperations.Length < 1
                //    || inputOperations[0].Result() is not DataTable inputTable)
                //    return null;

                //inputTable = inputTable.Copy();

                //if (inputOperations.Length == 1)
                //    inputTable.Columns.Add(ColumnName);

                //if (inputOperations.Length == 2)
                //    inputTable.Columns
                //        .Add(ColumnName, inputOperations[1].GetType())
                //        .DefaultValue = inputOperations[1];

                //return inputTable;

                return null;
            });
        }
    }
}
