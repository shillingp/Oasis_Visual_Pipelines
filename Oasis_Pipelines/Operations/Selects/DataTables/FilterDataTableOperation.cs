using System.Data;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Oasis_Pipelines.Classes;
using Oasis_Pipelines.Functions;

namespace Oasis_Pipelines.Operations.Selects.DataTables;

public sealed class FilterDataTableBlockDiagramOperation : BlockOperation
{

    public override string OperationTitle => "Filter Table";

    public bool FilterAny { get; set; } = false;

    public Dictionary<string, Type> Columns { get; set; } = new Dictionary<string, Type>();
    public ObservableSet<DataTableFilter> SelectedFilters { get; set; } = new ObservableSet<DataTableFilter>();

    public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
    {
        return new BlockOperationResult(additionalOperations =>
        {
            BlockOperationResult? tableOperation = inputOperations.Concat(additionalOperations)
                .FirstOrDefault(operation => operation.CalculateResult() is DataTable);

            if (tableOperation?.CalculateResult() is not DataTable tableObject) return null;

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