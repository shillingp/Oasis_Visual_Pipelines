using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Functions;
using Oasis_Visual_Pipelines.Interfaces;
using PropertyChanged;
using System.Data;
using System.Diagnostics;

namespace Oasis_Visual_Pipelines.Operations
{
    [AddINotifyPropertyChangedInterface]
    [BlockOperationGroup(BlockOperationType.DataTable, BlockOperationGroup.Transforms)]
    public class UpdateColumnBlockDiagramOperation : IBlockDiagramOperation
    {
        public int MaxInputs => 2;

        public string OperationTitle => "Update Column";

        public string ColumnName { get; set; } = null;

        public string[] ValidColumns { get; set; }

        public BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            BlockOperationResult? dataTableInput = inputOperations.FirstOrDefault(operation => operation.Result() is DataTable);
            BlockOperationResult? updateFunctionInput = inputOperations.FirstOrDefault(operation => operation != dataTableInput);

            if (dataTableInput?.Result() is not DataTable dataTable)
                return BlockOperationResult.NullOperation;

            ValidColumns = HelperFunctions.ExtractColumnNamesFromTable(dataTable);

            return new BlockOperationResult(additionalOperations =>
            {
                if (updateFunctionInput is null || ColumnName is null || !dataTable.Columns.Contains(ColumnName))
                    return dataTable;

                if (dataTable.Rows[0][ColumnName] is Array)
                    Debugger.Break();

                DataTable inputTable = dataTable.Copy();

                foreach (DataRow tableRow in inputTable.Rows)
                    tableRow[ColumnName] = updateFunctionInput.Result(
                        new BlockOperationResult(tableRow[ColumnName]));

                return inputTable;
            });
        }
    }
}
