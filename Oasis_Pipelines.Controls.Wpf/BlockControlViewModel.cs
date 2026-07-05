using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Oasis_Pipelines.Model;
using Oasis_Pipelines.Shared.Wpf.Enums;
using Oasis_Pipelines.Shared.Wpf.Interfaces;
using Oasis_Pipelines.Shared.Wpf.Services;

namespace Oasis_Pipelines.Controls.Wpf;

public class BlockControlViewModel
{
    public Block? Block { get; set; }

    public bool IsExpanded { get; set; }
    public bool IsSelected { get; set; }
    public object ConnectorDragController { get; }

    public ICommand RemoveBlockCommand { get; }
    public ICommand ToggleBlockHeightCommand { get; }

    public BlockControlViewModel([FromKeyedServices(DragControllerType.Connector)] IDragController connectorDragController)
    {
        ConnectorDragController = connectorDragController;

        RemoveBlockCommand = new RelayCommand(RemoveBlock);
        ToggleBlockHeightCommand = new RelayCommand(ToggleBlockHeight);
    }

    private void RemoveBlock()
    {
        throw new NotImplementedException();
    }

    private void ToggleBlockHeight()
    {
        throw new NotImplementedException();
    }
}