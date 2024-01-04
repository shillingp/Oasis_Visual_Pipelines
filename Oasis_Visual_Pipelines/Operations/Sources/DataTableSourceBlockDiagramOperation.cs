using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Interfaces;
using PropertyChanged;
using System.Data;

namespace Oasis_Visual_Pipelines.Operations
{
    [AddINotifyPropertyChangedInterface]
    [BlockOperationGroup(BlockOperationType.DataTable, BlockOperationGroup.Sources)]
    public class DataTableSourceBlockDiagramOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => 0;
        public override string OperationTitle => "Table Source";

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            return new BlockOperationResult(additionalOperations =>
            {
                DataTable exampleDataTable = new DataTable();

                exampleDataTable.Columns.Add("Index", typeof(int));
                exampleDataTable.Columns.Add("First Name", typeof(string));
                exampleDataTable.Columns.Add("Last Name", typeof(string));

                exampleDataTable.Rows.Add(1, "Peter", "Shilling");
                exampleDataTable.Rows.Add(2, "John", "Smith");
                exampleDataTable.Rows.Add(3, "Joe", "Bloggs");

                return exampleDataTable;
            });
        }
    }
}
