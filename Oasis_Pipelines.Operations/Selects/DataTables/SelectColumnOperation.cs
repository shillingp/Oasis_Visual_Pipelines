using System.Collections.Immutable;
using System.ComponentModel;
using System.Data;
using Oasis_Pipelines.Operations.Attributes;
using Oasis_Pipelines.Operations.Classes;
using Oasis_Pipelines.Operations.Enums;
using Oasis_Pipelines.Operations.Functions;
using PropertyChanged;

namespace Oasis_Pipelines.Operations.Selects.DataTables;

[AddINotifyPropertyChangedInterface]
[BlockOperationGroup(BlockOperationType.DataTable, BlockOperationGrouping.Select)]
public sealed class SelectColumnOperation : BlockOperation
{
    public override string OperationTitle => "Select Column";

    [DoNotReflowOnPropertyChanged]
    public string[]? ValidColumns { get; set; } = null;
    public ImmutableHashSet<object> SelectedColumns { get; set; } = ImmutableHashSet<object>.Empty;

    protected override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        BlockOperationResult? dataTableInput = inputOperations.FirstOrDefault(operation => operation.Result() is DataTable);

        if (dataTableInput?.Result() is not DataTable dataTable)
        {
            ValidColumns = null;
            return BlockOperationResult.NullOperation;
        }

        ValidColumns ??= DataTableFunctions.ExtractColumnNamesFromTable(dataTable);

        if (SelectedColumns.IsEmpty)
            return BlockOperationResult.NullOperation;

        string[] selectedColumnsRetainingOrder = ValidColumns
            .Intersect(SelectedColumns.Cast<string>())
            .ToArray();

        return new BlockOperationResult(additionalOperations =>
            new DataView(dataTable).ToTable(false, selectedColumnsRetainingOrder));
    }
}