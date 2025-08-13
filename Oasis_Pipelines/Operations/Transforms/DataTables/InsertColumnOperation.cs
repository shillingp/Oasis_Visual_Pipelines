using System.Data;
using Oasis_Pipelines.Classes;

namespace Oasis_Pipelines.Operations.Transforms.DataTables;


public sealed class InsertColumnOperation : BlockOperation
{
    public override string OperationTitle => "Insert Column";

    public string? ColumnName { get; set; }

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        BlockOperationResult? dataTableInput = inputOperations.FirstOrDefault(operation => operation.CalculateResult() is DataTable);
        BlockOperationResult? insertValueInput = inputOperations.FirstOrDefault(operation => operation != dataTableInput);

        if (dataTableInput?.CalculateResult() is not DataTable dataTable)
            return BlockOperationResult.NullOperation;

        return new BlockOperationResult(additionalOperations =>
        {
            DataTable inputTable = dataTable.Copy();

            if (ColumnName is null)
                return inputTable;


            if (insertValueInput is null)
                return inputTable;

            dynamic? insertData = insertValueInput.Value.CalculateResult();
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