using System.Windows;
using System.Windows.Controls.Primitives;

namespace Oasis_Pipelines.Shared.Wpf.Interfaces;

public interface IDragController
{
    public void StartDrag(FrameworkElement element, DragStartedEventArgs dragStartedEventArgs);
    
    public void Drag(FrameworkElement element, DragDeltaEventArgs dragDeltaEventArgs);
    
    public void StopDrag(FrameworkElement element, DragCompletedEventArgs dragCompletedEventArgs);
}