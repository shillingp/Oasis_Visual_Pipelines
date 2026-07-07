using System.Drawing;
using System.Windows.Controls.Primitives;
using Oasis_Pipelines.Model;
using Oasis_Pipelines.Operations.Enums;

namespace Oasis_Pipelines.Shared.Wpf.Interfaces.Dragging;

public interface IConnectionDragController
{
    void StartDrag(PointF startPoint, ConnectionSide dragSide, DragStartedEventArgs dragStartedEventArgs);
    void StartDrag(Connection connector, ConnectionSide dragSide, DragStartedEventArgs dragStartedEventArgs);
    void UpdateDrag(DragDeltaEventArgs dragDeltaEventArgs);
    void StopDrag(DragCompletedEventArgs dragCompletedEventArgs);
}