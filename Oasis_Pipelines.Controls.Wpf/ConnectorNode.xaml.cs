using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Oasis_Pipelines.Model;
using Oasis_Pipelines.Operations.Enums;
using Oasis_Pipelines.Shared.Wpf.Interfaces;
using Oasis_Pipelines.Shared.Wpf.Services;

namespace Oasis_Pipelines.Controls.Wpf;

public partial class ConnectorNode : UserControl
{
    public static readonly int ConnectorNodeSize = 10;

    #region Dependancy Properties

    public Connection Connection
    {
        get { return (Connection)GetValue(ConnectionProperty); }
        set { SetValue(ConnectionProperty, value); }
    }

    public static readonly DependencyProperty ConnectionProperty =
        DependencyProperty.Register(
            nameof(Connection),
            typeof(Connection),
            typeof(ConnectorNode),
            new PropertyMetadata(null));

    public ConnectionSide ConnectionSide
    {
        get { return (ConnectionSide)GetValue(ConnectionSideProperty); }
        set { SetValue(ConnectionSideProperty, value); }
    }

    public static readonly DependencyProperty ConnectionSideProperty =
        DependencyProperty.Register(
            nameof(ConnectionSide),
            typeof(ConnectionSide),
            typeof(ConnectorNode),
            new PropertyMetadata(null));

    public IDragController? DragController
    {
        get { return (IDragController?)GetValue(DragControllerProperty); }
        set { SetValue(DragControllerProperty, value); }
    }

    public static readonly DependencyProperty DragControllerProperty =
        DependencyProperty.Register(
            nameof(DragController),
            typeof(IDragController),
            typeof(ConnectorNode),
            new PropertyMetadata(null));
    #endregion

    public ConnectorNode()
    {
        InitializeComponent();
    }

    #region Events
    private void ConnectorThumb_DragStarted(object sender, DragStartedEventArgs e) => DragController?.StartDrag(this, e);
    private void ConnectorThumb_DragDelta(object sender, DragDeltaEventArgs e) => DragController?.Drag(this, e);
    private void ConnectorThumb_DragCompleted(object sender, DragCompletedEventArgs e) => DragController?.StopDrag(this, e);
    #endregion
}