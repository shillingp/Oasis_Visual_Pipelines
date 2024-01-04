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
    [BlockOperationGroup(BlockOperationType.DataTable, BlockOperationGroup.Sources)]
    public class DistinctDataTableBlockOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => 1;
        public override string OperationTitle => "Distinct Rows";

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            BlockOperationResult? tableOperation = inputOperations
                .FirstOrDefault(operation => operation?.Result() is DataTable);

            if (tableOperation?.Result() is not DataTable inputTable)
                return null;

            return new BlockOperationResult(additionalOperations =>
            {
                return inputTable.DefaultView.ToTable(true,
                    DataTableFunctions.ExtractColumnNamesFromTable(inputTable));
            });
        }
    }
}
