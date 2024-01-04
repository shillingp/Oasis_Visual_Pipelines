using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Functions;
using Oasis_Visual_Pipelines.Interfaces;
using PropertyChanged;
using System.Data;

namespace Oasis_Visual_Pipelines.Operations
{
    [BlockOperationGroup(BlockOperationType.DataTable, BlockOperationGroup.Select)]
    public class SelectColumnBlockDiagramOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => 1;
        public override string OperationTitle => "Select Column";

        public string[] ValidColumns { get; set; } = [];
        public HashSet<object> SelectedColumns { get; set; } = new HashSet<object>();

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            BlockOperationResult? dataTableInput = inputOperations.FirstOrDefault(operation => operation.Result() is DataTable);

            if (dataTableInput?.Result() is not DataTable dataTable)
            {
                ValidColumns = [];
                return BlockOperationResult.NullOperation;
            }

            ValidColumns = DataTableFunctions.ExtractColumnNamesFromTable(dataTable);

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
