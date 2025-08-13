using System.Data;
using Oasis_Pipelines.Classes;
using Oasis_Pipelines.Functions;

namespace Oasis_Pipelines.Operations.Transforms.DataTables;


public sealed class UpdateColumnOperation : BlockOperation
{
    public override string OperationTitle => "Update Column";

    public string? ColumnName { get; set; }

    public string[] ValidColumns { get; set; } = [];

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        BlockOperationResult? dataTableInput = inputOperations.FirstOrDefault(operation => operation.CalculateResult() is DataTable);
        BlockOperationResult? updateFunctionInput = inputOperations.FirstOrDefault(operation => operation != dataTableInput);

        if (dataTableInput?.CalculateResult() is not DataTable dataTable)
            return BlockOperationResult.NullOperation;

        ValidColumns = DataTableFunctions.ExtractColumnNamesFromTable(dataTable);

        return new BlockOperationResult(additionalOperations =>
        {
            if (updateFunctionInput is null || ColumnName is null || !dataTable.Columns.Contains(ColumnName))
                return dataTable;

            DataTable inputTable = dataTable.Copy();

            DataColumn? temporaryColumn = null;

            foreach (DataRow tableRow in inputTable.Rows)
            {
                IEnumerable<BlockOperationResult> innerBlockOperations = Array
                    .Empty<BlockOperationResult>()
                    .Append(new BlockOperationResult(tableRow[ColumnName]));

                if (tableRow[ColumnName] is Array array)
                    innerBlockOperations = array
                        .Cast<dynamic>()
                        .Select(element => new BlockOperationResult(element));

                dynamic? updateFunctionData = updateFunctionInput.Value.CalculateResult(innerBlockOperations.ToArray());

                if (updateFunctionData is null)
                    return dataTable;

                if (temporaryColumn is null && updateFunctionData is not null)
                {
                    Type updateFunctionType = updateFunctionData.GetType();
                    temporaryColumn = inputTable.Columns.Add("$$__Temporary__Column__$$", updateFunctionType);
                }

                tableRow[temporaryColumn!] = updateFunctionData;
            }

            if (temporaryColumn is null)
                return dataTable;

            int existingColumnPosition = inputTable.Columns[ColumnName]!.Ordinal;

            inputTable.Columns.Remove(ColumnName);
            temporaryColumn!.ColumnName = ColumnName;

            temporaryColumn.SetOrdinal(existingColumnPosition);

            return inputTable;
        });
    }
}