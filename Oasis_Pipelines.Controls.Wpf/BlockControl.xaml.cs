using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Oasis_Pipelines.Controls.Wpf.Classes;
using Oasis_Pipelines.Controls.Wpf.Interfaces;
using Oasis_Pipelines.Model;

namespace Oasis_Pipelines.Controls.Wpf;

public partial class BlockControl : UserControl
{
    private readonly BlockControlViewModel _viewModel;
    private readonly IConnectorVisualRegistry _connectorVisualRegistry;

    public Block? Block
    {
        get => (Block?)GetValue(BlockProperty);
        set => SetValue(BlockProperty, value);
    }

    public static readonly DependencyProperty BlockProperty =
        DependencyProperty.Register(
            nameof(Block),
            typeof(Block),
            typeof(BlockControl),
            new PropertyMetadata(null, OnBlockChanged));

    public BlockControl()
    {
        InitializeComponent();

        _connectorVisualRegistry = ControlServiceProvider.GetRequiredService<IConnectorVisualRegistry>();
        _viewModel = ControlServiceProvider.GetRequiredService<BlockControlViewModel>();
        RootGrid.DataContext = _viewModel;
    }

    private static void OnBlockChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not BlockControl blockControl || e.NewValue is not Block block)
            return;

        blockControl._viewModel.Block = block;
    }

    private void TopLevelCard_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        e.Handled = false;

        // This is where selection is handled. Need to implement through the SessionContext
        // WeakReferenceMessenger.Default.Send(new BlockControlSelectionMessage(this));
    }

    private void Thumb_OnDragDelta(object sender, DragDeltaEventArgs e)
    {
        Block?.Position = new PointF
        {
            X = Block.Position.X + (float)e.HorizontalChange,
            Y = Block.Position.Y + (float)e.VerticalChange
        };
    }
}