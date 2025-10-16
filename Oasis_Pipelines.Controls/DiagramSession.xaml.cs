using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Oasis_Pipelines.Interfaces;
using Oasis_Pipelines.Services.SessionManagement;

namespace Oasis_Pipelines.Controls;

public partial class DiagramSession : UserControl
{
    public ISessionContext SessionContext
    {
        get => (ISessionContext)GetValue(SessionContextProperty);
        set => SetValue(SessionContextProperty, value);
    }

    public static readonly DependencyProperty SessionContextProperty =
        DependencyProperty.Register(
            nameof(SessionContext),
            typeof(ISessionContext),
            typeof(DiagramSession),
            new PropertyMetadata(null));

    public DiagramSession()
    {
        InitializeComponent();
    }

    private void Thumb_OnDragDelta(object sender, DragDeltaEventArgs e)
    {
        IPipelineObject? pipelineObject = (sender as Thumb)?.DataContext as IPipelineObject;
        pipelineObject?.Position = new PointF
        {
            X = pipelineObject.Position.X + (float)e.HorizontalChange,
            Y = pipelineObject.Position.Y - (float)e.VerticalChange
        };
    }
}