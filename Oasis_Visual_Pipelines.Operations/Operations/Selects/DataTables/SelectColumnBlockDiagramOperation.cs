﻿using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Functions;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Data;

namespace Oasis_Visual_Pipelines.Operations.Selects.DataTables
{
    [BlockOperationGroup(BlockOperationType.DataTable, BlockOperationGrouping.Select)]
    public sealed class SelectColumnBlockDiagramOperation : BaseBlockDiagramOperation, INotifyPropertyChanged
    {
        public override int MaxInputs => 1;
        public override string OperationTitle => "Select Column";

        [DoNotReflowOnPropertyChanged]
        public string[]? ValidColumns { get; set; } = null;
        public ImmutableHashSet<object> SelectedColumns { get; set; } = ImmutableHashSet<object>.Empty;

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            BlockOperationResult? dataTableInput = inputOperations.FirstOrDefault(operation => operation.Result() is DataTable);

            if (dataTableInput?.Result() is not DataTable dataTable)
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
}
