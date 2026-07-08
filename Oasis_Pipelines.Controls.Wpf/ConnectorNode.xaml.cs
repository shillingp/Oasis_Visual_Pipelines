using System.Windows;
using System.Windows.Controls;
using Oasis_Pipelines.Controls.Wpf.Classes;
using Oasis_Pipelines.Controls.Wpf.Interfaces;
using Oasis_Pipelines.Model;
using Oasis_Pipelines.Operations.Enums;

namespace Oasis_Pipelines.Controls.Wpf;

public partial class ConnectorNode : UserControl
{
    public static readonly int ConnectorNodeSize = 10;

    private readonly IConnectorVisualRegistry _connectorVisualRegistry;
    private readonly ConnectorNodeViewModel _viewModel;

    #region Dependancy Properties

    public Connection? Connection
    {
        get { return (Connection?)GetValue(ConnectionProperty); }
        set { SetValue(ConnectionProperty, value); }
    }

    public static readonly DependencyProperty ConnectionProperty =
        DependencyProperty.Register(
            nameof(Connection),
            typeof(Connection),
            typeof(ConnectorNode),
            new PropertyMetadata(null, OnConnectionChanged));

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

    public Block? Block
    {
        get => (Block?)GetValue(BlockProperty);
        set => SetValue(BlockProperty, value);
    }

    public static readonly DependencyProperty BlockProperty =
        DependencyProperty.Register(
            nameof(Block),
            typeof(Block),
            typeof(ConnectorNode),
            new PropertyMetadata(null, OnBlockChanged));

    #endregion

    public ConnectorNode()
    {
        InitializeComponent();

        _connectorVisualRegistry = ControlServiceProvider.GetRequiredService<IConnectorVisualRegistry>();
        _viewModel = ControlServiceProvider.GetRequiredService<ConnectorNodeViewModel>();
        RootGrid.DataContext = _viewModel;

        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (Block is not null)
            _connectorVisualRegistry.Register(this);
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        if (Block is not null)
            _connectorVisualRegistry.Unregister(this);
    }

    private static void OnConnectionChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        if (dependencyObject is not ConnectorNode connectorNode || e.NewValue is not Connection connection)
            return;
        connectorNode._viewModel.Connection = connection;
    }

    private static void OnBlockChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        if (dependencyObject is not ConnectorNode connectorNode || e.NewValue is not Block block)
            return;
        connectorNode._viewModel.Block = block;
    }
}