using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Functions;
using PropertyChanged;
using System.Data;

namespace Oasis_Visual_Pipelines.Operations.Joins.DataTables
{
    [BlockOperationGroup(BlockOperationType.DataTable, BlockOperationGroup.Join)]
    public class JoinDataTablesBlockDiagramOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => 2;
        public override string OperationTitle => "Join Tables";

        [DoNotReflowOnPropertyChanged]
        public string[] LeftColumns { get; set; } = [];
        [DoNotReflowOnPropertyChanged]
        public string[] RightColumns { get; set; } = [];

        public string? SelectedLeftColumn { get; set; }
        public string? SelectedRightColumn { get; set; }

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            BlockOperationResult? leftDataTableInput = inputOperations.FirstOrDefault(operation => operation.Result() is DataTable);
            BlockOperationResult? rightDataTableInput = inputOperations.FirstOrDefault(operation => operation != leftDataTableInput);

            if (leftDataTableInput?.Result() is not DataTable leftDataTable)
                return BlockOperationResult.NullOperation;

            LeftColumns = DataTableFunctions.ExtractColumnNamesFromTable(leftDataTable);

            return new BlockOperationResult(additionalOperations =>
            {
                if (rightDataTableInput?.Result() is not DataTable rightDataTable)
                    return leftDataTable;

                RightColumns = DataTableFunctions.ExtractColumnNamesFromTable(rightDataTable);

                if (SelectedLeftColumn is null || SelectedRightColumn is null)
                    return BlockOperationResult.NullOperation;

                if (leftDataTable.Columns[SelectedLeftColumn]?.DataType != rightDataTable.Columns[SelectedRightColumn]?.DataType)
                    return new FailedOperation("Column data types must match!");

                return DataTableFunctions.JoinDataTables(
                    leftDataTable,
                    rightDataTable,
                    SelectedLeftColumn,
                    SelectedRightColumn);
            });
        }
    }
}
