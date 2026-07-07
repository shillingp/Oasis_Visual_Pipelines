using System.Windows.Controls.Primitives;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Oasis_Pipelines.Controls.Wpf.Interfaces;
using Oasis_Pipelines.Model;
using Oasis_Pipelines.Operations.Enums;
using Oasis_Pipelines.Shared.Wpf.Enums;
using Oasis_Pipelines.Shared.Wpf.Extensions;
using Oasis_Pipelines.Shared.Wpf.Interfaces.Dragging;

namespace Oasis_Pipelines.Controls.Wpf;

public class ConnectorNodeViewModel
{
    private readonly IConnectionDragController _connectorDragController;
    private readonly IConnectorVisualRegistry _connectorVisualRegistry;

    public Block? Block { get; set; }
    public Connection? Connection { get; set; }
    public ConnectionSide ConnectionSide { get; set; }

    public ICommand StartDragCommand { get; }
    public ICommand UpdateDragCommand { get; }
    public ICommand StopDragCommand { get; }

    public ConnectorNodeViewModel(
        IConnectionDragController connectorDragController,
        IConnectorVisualRegistry connectorVisualRegistry)
    {
        _connectorDragController = connectorDragController;
        _connectorVisualRegistry = connectorVisualRegistry;

        StartDragCommand = new RelayCommand<DragStartedEventArgs>(ConnectorThumb_DragStarted);
        UpdateDragCommand = new RelayCommand<DragDeltaEventArgs>(ConnectorThumb_DragDelta);
        StopDragCommand = new RelayCommand<DragCompletedEventArgs>(ConnectorThumb_DragCompleted);
    }

    private void ConnectorThumb_DragStarted(DragStartedEventArgs? eventArgs)
    {
        if (eventArgs is null || Block is null)
            return;

        if (Connection is not null)
            _connectorDragController.StartDrag(Connection, ConnectionSide, eventArgs);
        else if (eventArgs.Source is Thumb thumb)
            _connectorDragController.StartDrag(thumb.GetFrameworkElementCenter(), ConnectionSide, eventArgs);
    }

    private void ConnectorThumb_DragDelta(DragDeltaEventArgs? eventArgs)
    {
        if (eventArgs is null) return;
        _connectorDragController.UpdateDrag(eventArgs);
    }

    private void ConnectorThumb_DragCompleted(DragCompletedEventArgs? eventArgs)
    {
        if (eventArgs is null) return;
        _connectorDragController.StopDrag(eventArgs);
    }
}