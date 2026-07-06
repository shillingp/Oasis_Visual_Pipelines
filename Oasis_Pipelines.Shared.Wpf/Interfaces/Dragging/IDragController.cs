using System.Drawing;
using System.Windows.Controls.Primitives;

namespace Oasis_Pipelines.Shared.Wpf.Interfaces.Dragging;

public interface IDragController
{
    void StartDrag(PointF startingPosition, DragStartedEventArgs dragStartedEventArgs);
    void UpdateDrag(DragDeltaEventArgs dragDeltaEventArgs);
    void StopDrag(DragCompletedEventArgs dragCompletedEventArgs);
}