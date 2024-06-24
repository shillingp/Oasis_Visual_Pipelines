using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Functions;
using System.Data;

namespace Oasis_Visual_Pipelines.Operations.Selects.DataTables
{
    [BlockOperationGroup(BlockOperationType.DataTable, BlockOperationGrouping.Sources)]
    public class DistinctDataTableBlockOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => 1;
        public override string OperationTitle => "Distinct Rows";

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            BlockOperationResult? tableOperation = inputOperations
                .FirstOrDefault(operation => operation?.Result() is DataTable);

            if (tableOperation?.Result() is not DataTable inputTable)
                return BlockOperationResult.NullOperation;

            return new BlockOperationResult(additionalOperations =>
            {
                return inputTable.DefaultView.ToTable(true,
                    DataTableFunctions.ExtractColumnNamesFromTable(inputTable));
            });
        }
    }
}
