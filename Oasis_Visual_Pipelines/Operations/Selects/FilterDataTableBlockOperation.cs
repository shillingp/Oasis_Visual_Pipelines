using CommunityToolkit.Mvvm.Input;
using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Dialogs;
using Oasis_Visual_Pipelines.Functions;
using PropertyChanged;
using System.Data;
using System.Windows.Input;

namespace Oasis_Visual_Pipelines.Operations
{
    [BlockOperationGroup(Enums.BlockOperationType.DataTable, Enums.BlockOperationGroup.Select)]
    public class FilterDataTableBlockDiagramOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => 1;
        public override string OperationTitle => "Filter Table";

        public bool FilterAny { get; set; } = false;

        [DoNotReflowOnPropertyChanged]
        public Dictionary<string, Type> Columns { get; set; } = new Dictionary<string, Type>();
        public ObservableSet<DataTableFilter> SelectedFilters { get; set; } = new ObservableSet<DataTableFilter>();

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            return new BlockOperationResult(additionalOperations =>
            {
                BlockOperationResult? tableOperation = inputOperations.Concat(additionalOperations)
                    .FirstOrDefault(operation => operation.Result() is DataTable);

                if (tableOperation?.Result() is not DataTable tableObject) return null;

                Columns = DataTableFunctions.ExtractColumnsFromTable(tableObject)
                    .ToDictionary(
                        column => column.ColumnName,
                        column => column.DataType);

                return DataTableFunctions.FilterDataTable(
                    tableObject,
                    SelectedFilters,
                    FilterAny ? "OR" : "AND");
            });
        }

        public ICommand EditFiltersCommand => new RelayCommand(async () =>
        {
            await DialogHostFunctions.CreateAndShowDialog(
                new FilterSelectionDialog(),
                this,
                closeOnClickAway: true);
        });

        public ICommand AddNewFilterCommand => new RelayCommand(
            () => SelectedFilters.Add(new DataTableFilter()));

        public ICommand RemoveFilterCommand => new RelayCommand<DataTableFilter>(
            filterToRemove => SelectedFilters.Remove(filterToRemove!));
    }
}
