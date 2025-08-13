using System.Collections.Immutable;
using System.ComponentModel;
using System.Data;
using Oasis_Pipelines.Classes;
using Oasis_Pipelines.Functions;

namespace Oasis_Pipelines.Operations.Selects.DataTables;


public sealed class SelectColumnOperation : BlockOperation
{

    public override string OperationTitle => "Select Column";

    private string[]? ValidColumns { get; set; } = null;
    private ImmutableHashSet<object> SelectedColumns { get; set; } = ImmutableHashSet<object>.Empty;

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        BlockOperationResult? dataTableInput = inputOperations.FirstOrDefault(operation => operation.CalculateResult() is DataTable);

        if (dataTableInput?.CalculateResult() is not DataTable dataTable)
        {
            ValidColumns = null;
            return BlockOperationResult.NullOperation;
        }

        ValidColumns ??= DataTableFunctions.ExtractColumnNamesFromTable(dataTable);

        if (SelectedColumns is null || !SelectedColumns.Any())
            return BlockOperationResult.NullOperation;

        string[] selectedColumnsRetainingOrder = ValidColumns
            .Intersect(SelectedColumns.Cast<string>())
            .ToArray();

        return new BlockOperationResult(additionalOperations =>
            new DataView(dataTable).ToTable(false, selectedColumnsRetainingOrder));
    }
}