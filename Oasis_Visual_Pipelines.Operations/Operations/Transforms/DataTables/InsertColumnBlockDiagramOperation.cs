using Oasis_Visual_Pipelines.Operations.Attributes;
using Oasis_Visual_Pipelines.Operations.Classes;
using Oasis_Visual_Pipelines.Operations.Enums;
using System.Data;

namespace Oasis_Visual_Pipelines.Operations.Transforms.DataTables
{
    [BlockOperationGroup(BlockOperationType.DataTable, BlockOperationGroup.Transforms)]
    public class InsertColumnBlockDiagramOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => 2;
        public override string OperationTitle => "Insert Column";

        public string? ColumnName { get; set; }

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            BlockOperationResult? dataTableInput = inputOperations.FirstOrDefault(operation => operation.Result() is DataTable);
            BlockOperationResult? insertValueInput = inputOperations.FirstOrDefault(operation => operation != dataTableInput);

            if (dataTableInput?.Result() is not DataTable dataTable)
                return BlockOperationResult.NullOperation;

            return new BlockOperationResult(additionalOperations =>
            {
                DataTable inputTable = dataTable.Copy();

                if (ColumnName is null)
                    return inputTable;


                if (insertValueInput is null)
                    return inputTable;

                dynamic? insertData = insertValueInput.Result();
                Type? insertDataType = insertData?.GetType();
                if (insertDataType is null)
                    return inputTable;

                DataColumn insertedColumn = inputTable.Columns.Add(ColumnName, insertDataType);

                foreach (DataRow tableRow in inputTable.Rows)
                    tableRow[insertedColumn] = insertData;

                return inputTable;
            });
        }
    }
}
