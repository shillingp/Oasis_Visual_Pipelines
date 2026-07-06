using System.Windows.Controls.Primitives;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Oasis_Pipelines.Model;
using Oasis_Pipelines.Shared.Wpf.Enums;
using Oasis_Pipelines.Shared.Wpf.Extensions;
using Oasis_Pipelines.Shared.Wpf.Interfaces;
using Oasis_Pipelines.Shared.Wpf.Interfaces.Dragging;

namespace Oasis_Pipelines.Controls.Wpf;

public class ConnectorNodeViewModel
{
    private readonly IDragController _connectorDragController;

    public ConnectorNode? ConnectorNode { get; set; }
    public Block? Block { get; set; }
    public Connection? Connection { get; set; }

    public ICommand StartDragCommand { get; }
    public ICommand UpdateDragCommand { get; }
    public ICommand StopDragCommand { get; }

    public ConnectorNodeViewModel(
        [FromKeyedServices(DragControllerType.Connector)]
        IDragController connectorDragController)
    {
        _connectorDragController = connectorDragController;

        StartDragCommand = new RelayCommand<DragStartedEventArgs>(ConnectorThumb_DragStarted);
        UpdateDragCommand = new RelayCommand<DragDeltaEventArgs>(ConnectorThumb_DragDelta);
        StopDragCommand = new RelayCommand<DragCompletedEventArgs>(ConnectorThumb_DragCompleted);
    }

    private void ConnectorThumb_DragStarted(DragStartedEventArgs? eventArgs)
    {
        if (ConnectorNode?.GetFrameworkElementCenter() is not { } connectorCenter || eventArgs is null) return;
        _connectorDragController.StartDrag(connectorCenter, eventArgs);
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