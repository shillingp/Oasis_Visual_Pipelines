using System.Data;
using Oasis_Pipelines.Classes;

namespace Oasis_Pipelines.Operations.Selects.DataTables;


public sealed class RemoveBlanksOperation : BlockOperation
{

    public override string OperationTitle => "Remove Blanks";

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        BlockOperationResult? tableOperation = inputOperations
            .FirstOrDefault(operation => operation.CalculateResult() is DataTable);

        if (tableOperation?.CalculateResult() is not DataTable inputTable)
            return BlockOperationResult.NullOperation;

        return new BlockOperationResult(additionalOperations => inputTable.Rows
            .Cast<DataRow>()
            .Where(row => !row.ItemArray.All(field => field is DBNull || string.IsNullOrEmpty(field as string)))
            .CopyToDataTable());
    }
}