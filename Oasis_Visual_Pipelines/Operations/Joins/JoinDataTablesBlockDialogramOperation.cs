using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Functions;
using Oasis_Visual_Pipelines.Interfaces;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oasis_Visual_Pipelines.Operations
{
    [AddINotifyPropertyChangedInterface]
    [BlockOperationGroup(BlockOperationType.DataTable, BlockOperationGroup.Join)]
    internal class JoinDataTablesBlockDiagramOperation : IBlockDiagramOperation
    {
        public int MaxInputs => 2;
        public string OperationTitle => "Join Tables";

        public string[] LeftColumns { get; set; } = [];
        public string[] RightColumns { get; set; } = [];

        public string? SelectedLeftColumn { get; set; }
        public string? SelectedRightColumn { get; set; }

        public BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
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

                return DataTableFunctions.JoinDataTable(
                    leftDataTable, rightDataTable,
                    SelectedLeftColumn, SelectedRightColumn);
            });
        }
    }
}
