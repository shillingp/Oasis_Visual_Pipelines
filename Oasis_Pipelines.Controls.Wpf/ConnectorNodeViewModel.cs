using System.Windows.Controls.Primitives;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Oasis_Pipelines.Model;
using Oasis_Pipelines.Operations.Enums;
using Oasis_Pipelines.Shared.Wpf.Enums;
using Oasis_Pipelines.Shared.Wpf.Extensions;
using Oasis_Pipelines.Shared.Wpf.Interfaces.Dragging;

namespace Oasis_Pipelines.Controls.Wpf;

public class ConnectorNodeViewModel
{
    private readonly IConnectionDragController _connectorDragController;

    public Block? Block { get; set; }
    public Connection? Connection { get; set; }
    public ConnectionSide ConnectionSide { get; set; }

    public ICommand StartDragCommand { get; }
    public ICommand UpdateDragCommand { get; }
    public ICommand StopDragCommand { get; }

    public ConnectorNodeViewModel(IConnectionDragController connectorDragController)
    {
        _connectorDragController = connectorDragController;

        StartDragCommand = new RelayCommand<DragStartedEventArgs>(ConnectorThumb_DragStarted);
        UpdateDragCommand = new RelayCommand<DragDeltaEventArgs>(ConnectorThumb_DragDelta);
        StopDragCommand = new RelayCommand<DragCompletedEventArgs>(ConnectorThumb_DragCompleted);
    }

    private void ConnectorThumb_DragStarted(DragStartedEventArgs? eventArgs)
    {
        if (eventArgs is null || Block is null || eventArgs.Source is not Thumb thumb)
            return;

        if (Connection is not null)
            _connectorDragController.StartDrag(Connection, ConnectionSide,
                Mouse.GetPosition(eventArgs.Source as Thumb).ToPointF());
        else
            _connectorDragController.StartDrag(thumb.GetFrameworkElementCenter(), ConnectionSide);
    }

    private void ConnectorThumb_DragDelta(DragDeltaEventArgs? eventArgs)
    {
        if (eventArgs is null) return;
        _connectorDragController.UpdateDrag(
            Mouse.GetPosition(eventArgs.Source as Thumb).ToPointF());
    }

    private void ConnectorThumb_DragCompleted(DragCompletedEventArgs? eventArgs)
    {
        if (eventArgs is null) return;
        _connectorDragController.StopDrag(
            Mouse.GetPosition(eventArgs.Source as Thumb).ToPointF());
    }
}