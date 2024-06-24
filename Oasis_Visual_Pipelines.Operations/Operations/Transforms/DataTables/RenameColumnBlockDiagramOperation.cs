using Oasis_Visual_Pipelines.Operations.Attributes;
using Oasis_Visual_Pipelines.Operations.Classes;
using Oasis_Visual_Pipelines.Functions;
using PropertyChanged;
using System.ComponentModel;
using System.Data;

namespace Oasis_Visual_Pipelines.Operations.Transforms.DataTables
{
    [BlockOperationGroup(Enums.BlockOperationType.DataTable, Enums.BlockOperationGroup.Transforms)]
    public class RenameColumnBlockDiagramOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => 1;
        public override string OperationTitle => "Rename Column";

        [DoNotReflowOnPropertyChanged]
        public string[]? ValidColumns { get; set; } = null;

        public string? SelectedColumn { get; set; }
        public string? NewColumnName { get; set; }

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            BlockOperationResult? leftDataTableInput = inputOperations.FirstOrDefault(operation => operation.Result() is DataTable);

            if (leftDataTableInput?.Result() is not DataTable inputDataTable)
            {
                ValidColumns = [];
                return BlockOperationResult.NullOperation;
            }

            ValidColumns = DataTableFunctions.ExtractColumnNamesFromTable(inputDataTable);

            return new BlockOperationResult(additionalOperations =>
            {
                if (SelectedColumn is null || NewColumnName is null || inputDataTable.Columns[SelectedColumn] is null)
                    return inputDataTable;

                inputDataTable.Columns[SelectedColumn]!.ColumnName = NewColumnName;

                return inputDataTable;
            });
        }
    }
}
