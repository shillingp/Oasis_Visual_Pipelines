using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Oasis_Pipelines.Operations;
using Oasis_Pipelines.Operations.Enums;
using Oasis_Pipelines.Services.SessionManagement;
using PropertyChanged;

namespace Oasis_Pipelines.Controls.Wpf;

[AddINotifyPropertyChangedInterface]
public class DiagramSessionViewModel
{
    private readonly BlockOperation _defaultBlockOperation;

    public ISessionContext? SessionContext { get; set; }
    public ICommand AddBlockCommand { get; }

    public DiagramSessionViewModel(
        [FromKeyedServices(BlockOperationGrouping.Other)] BlockOperation defaultBlockOperation)
    {
        _defaultBlockOperation = defaultBlockOperation;

        AddBlockCommand = new RelayCommand<MouseButtonEventArgs>(AddBlock);
    }

    private void AddBlock(MouseButtonEventArgs? e)
    {
        SessionContext?.BlockManager.AddBlock(_defaultBlockOperation);
    }
}