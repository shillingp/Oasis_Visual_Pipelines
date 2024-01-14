using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Functions;
using PropertyChanged;
using System.Data;

namespace Oasis_Visual_Pipelines.Operations.Transforms.DataTables
{
    [BlockOperationGroup(BlockOperationType.DataTable, BlockOperationGroup.Transforms)]
    public class UpdateColumnBlockDiagramOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => 2;
        public override string OperationTitle => "Update Column";

        public string? ColumnName { get; set; }

        [DoNotReflowOnPropertyChanged]
        public string[] ValidColumns { get; set; } = [];

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            BlockOperationResult? dataTableInput = inputOperations.FirstOrDefault(operation => operation.Result() is DataTable);
            BlockOperationResult? updateFunctionInput = inputOperations.FirstOrDefault(operation => operation != dataTableInput);

            if (dataTableInput?.Result() is not DataTable dataTable)
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

                    dynamic? updateFunctionData = updateFunctionInput.Result(innerBlockOperations.ToArray());

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
}
