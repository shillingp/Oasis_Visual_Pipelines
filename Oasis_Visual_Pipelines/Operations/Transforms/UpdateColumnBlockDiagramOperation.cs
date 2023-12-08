using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Functions;
using Oasis_Visual_Pipelines.Interfaces;
using PropertyChanged;
using System.Data;

namespace Oasis_Visual_Pipelines.Operations
{
    [AddINotifyPropertyChangedInterface]
    [BlockOperationGroup(BlockOperationType.DataTable, BlockOperationGroup.Transforms)]
    public class UpdateColumnBlockDiagramOperation : IBlockDiagramOperation
    {
        public int MaxInputs => 2;

        public string OperationTitle => "Update Column";

        public string ColumnName { get; set; } = null;

        public string[] ValidColumns { get; set; }

        public BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            BlockOperationResult? dataTableInput = inputOperations.FirstOrDefault(operation => operation.Result() is DataTable);
            BlockOperationResult? updateFunctionInput = inputOperations.FirstOrDefault(operation => operation != dataTableInput);

            if (dataTableInput?.Result() is not DataTable dataTable)
                return BlockOperationResult.NullOperation;

            ValidColumns = HelperFunctions.ExtractColumnNamesFromTable(dataTable);

            return new BlockOperationResult(additionalOperations =>
            {
                if (updateFunctionInput is null || ColumnName is null)
                    return dataTable;

                DataTable inputTable = dataTable.Copy();

                foreach (DataRow tableRow in inputTable.Rows)
                    tableRow[ColumnName] = updateFunctionInput.Result(
                        new BlockOperationResult(tableRow[ColumnName]);

                return inputTable;
            });
        }

        //public object Operation(object[] operationInputs, IBlockDiagramOperation[] inputOperations, Block block)
        //{
        //	DataTable dataTable = GetObjectsOfType<DataTable>(operationInputs)
        //		.FirstOrDefault();
        //	IBlockDiagramOperation blockOperation = GetObjectsOfType<IBlockDiagramOperation>(operationInputs)
        //		.FirstOrDefault();

        //	if (dataTable is not DataTable inputTable)
        //		return null;

        //	ValidColumns = inputTable.Columns
        //		.Cast<DataColumn>()
        //		.Select(column => column.ColumnName)
        //		.ToArray();

        //	inputTable = inputTable.Copy();

        //	if (operationInputs.Length == 1 || ColumnName.IsNullOrEmpty())
        //		return inputTable;

        //	foreach (DataRow tableRow in inputTable.Rows)
        //	{
        //		block.GetLeftInputs();

        //		//tableRow[ColumnName] = blockOperation.Operation(
        //		//	block.Ope)
        //		//tableRow[ColumnName] = blockOperation.Operation([tableRow[ColumnName]], [null], block);
        //	}

        //	return inputTable;
        //}

        //public T[] GetObjectsOfType<T>(object[] inputs)
        //{
        //	return inputs.OfType<T>().ToArray();
        //}
    }
}
