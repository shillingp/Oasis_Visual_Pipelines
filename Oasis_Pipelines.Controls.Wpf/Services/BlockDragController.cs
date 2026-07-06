using System.Drawing;
using System.Windows;
using System.Windows.Controls.Primitives;
using Oasis_Pipelines.Model;
using Oasis_Pipelines.Shared.Wpf.Interfaces;

namespace Oasis_Pipelines.Controls.Wpf.Services;

public class BlockDragController : IDragController
{
    public void StartDrag(PointF startingPosition, DragStartedEventArgs dragStartedEventArgs)
    {
        throw new NotImplementedException();
    }

    public void UpdateDrag(DragDeltaEventArgs dragDeltaEventArgs)
    {
        throw new NotImplementedException();
    }

    public void StopDrag(DragCompletedEventArgs dragCompletedEventArgs)
    {
        throw new NotImplementedException();
    }
}