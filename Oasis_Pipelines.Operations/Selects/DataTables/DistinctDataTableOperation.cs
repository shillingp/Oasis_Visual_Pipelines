using System.Data;
using Oasis_Pipelines.Classes;
using Oasis_Pipelines.Functions;

namespace Oasis_Pipelines.Operations.Selects.DataTables;


public sealed class DistinctDataTableOperation : BlockOperation
{

    public override string OperationTitle => "Distinct Rows";

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        BlockOperationResult? tableOperation = inputOperations
            .FirstOrDefault(operation => operation.CalculateResult() is DataTable);

        if (tableOperation?.CalculateResult() is not DataTable inputTable)
            return BlockOperationResult.NullOperation;

        return new BlockOperationResult(additionalOperations =>
        {
            return inputTable.DefaultView.ToTable(true,
                DataTableFunctions.ExtractColumnNamesFromTable(inputTable));
        });
    }
}