﻿using Oasis_Visual_Pipelines.Operations.Attributes;
using Oasis_Visual_Pipelines.Operations.Classes;
using Oasis_Visual_Pipelines.Operations.Enums;
using System.Data;

namespace Oasis_Visual_Pipelines.Operations.Selects.DataTables
{
    [BlockOperationGroup(BlockOperationType.DataTable, BlockOperationGroup.Select)]
    public class RemoveBlanksBlockDiagramOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => 1;
        public override string OperationTitle => "Remove Blanks";

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            BlockOperationResult? tableOperation = inputOperations
                .FirstOrDefault(operation => operation?.Result() is DataTable);

            if (tableOperation?.Result() is not DataTable inputTable)
                return BlockOperationResult.NullOperation;

            return new BlockOperationResult(additionalOperations => inputTable.Rows
                .Cast<DataRow>()
                .Where(row => !row.ItemArray.All(field => field is DBNull || string.IsNullOrEmpty(field as string)))
                .CopyToDataTable());
        }
    }
}
