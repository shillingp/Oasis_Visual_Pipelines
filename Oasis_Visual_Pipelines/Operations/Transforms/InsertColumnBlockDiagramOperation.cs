﻿using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Interfaces;
using System.Data;

namespace Oasis_Visual_Pipelines.Operations
{
    [BlockOperationGroup(BlockOperationType.DataTable, BlockOperationGroup.Transforms)]
    public class InsertColumnBlockDiagramOperation : IBlockDiagramOperation
    {
        public int MaxInputs => 2;

        public string OperationTitle => "Insert Column";

        public string ColumnName { get; set; }

        public BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            BlockOperationResult? dataTableInput = inputOperations.FirstOrDefault(operation => operation.Result() is DataTable);
            BlockOperationResult? insertValueInput = inputOperations.FirstOrDefault(operation => operation != dataTableInput);

            if (dataTableInput?.Result() is not DataTable dataTable)
                return BlockOperationResult.NullOperation;

            return new BlockOperationResult(additionalOperations =>
            {
                DataTable inputTable = dataTable.Copy();

                if (ColumnName is null)
                    return inputTable;

                DataColumn insertedColumn = inputTable.Columns.Add(ColumnName);

                if (insertValueInput is null)
                    return inputTable;

                foreach (DataRow tableRow in inputTable.Rows)
                    tableRow[insertedColumn] = insertValueInput.Result();

                return inputTable;
            });
        }
    }
}