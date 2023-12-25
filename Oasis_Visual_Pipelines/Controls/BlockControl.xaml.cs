using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Oasis_Visual_Pipelines.Classes.Messages;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Functions;
using Oasis_Visual_Pipelines.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Oasis_Visual_Pipelines.Controls
{
    /// <summary>
    /// Interaction logic for BlockControl.xaml
    /// </summary>
    public partial class BlockControl : UserControl, IRecipient<BlockControlSelectionMessage>
    {
        public Block Block
        {
            get { return (Block)GetValue(BlockProperty); }
            set { SetValue(BlockProperty, value); }
        }

        public static readonly DependencyProperty BlockProperty =
            DependencyProperty.Register(
                "Block",
                typeof(Block),
                typeof(BlockControl),
                new PropertyMetadata(null));

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register(
                "IsSelected",
                typeof(bool),
                typeof(BlockControl),
                new PropertyMetadata(false));

        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register(
                "IsExpanded",
                typeof(bool),
                typeof(BlockControl),
                new PropertyMetadata(false));

        public BlockControl(Block block)
        {
            WeakReferenceMessenger.Default.RegisterAll(this);

            InitializeComponent();

            Block = block;
        }

        #region Commands
        public ICommand ToggleBlockHeightCommand => new RelayCommand(() =>
        {
            MaxHeight = IsExpanded ? 100 : double.MaxValue;
            IsExpanded ^= true;

            Application.Current.Dispatcher.Invoke(Block.RedrawAnyConnections, DispatcherPriority.Render);
        });
        #endregion

        #region Methods
        #region Mouse Handling
        private void TopLevelCard_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = false;

            WeakReferenceMessenger.Default.Send(new BlockControlSelectionMessage(this));
        }

        #region Thumb Drag Handling
        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Point newPosition = new Point(
                Block.Position.X + e.HorizontalChange,
                Block.Position.Y + e.VerticalChange);

            Block.Position = UIHelperFunctions.ClipFrameworkElementPointWithinCanvas(this, newPosition);

            Block.RedrawAnyConnections();
        }

        private void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (Height is double.NaN) Height = ActualHeight;

            Height = Math.Max(MinHeight, Height + e.VerticalChange);

            Block.RedrawAnyConnections();
        }

        #region Connection Drag Handling
        private LooseConnection? drawingLooseConnection;
        private Block? looseConnectionSourceBlock;
        private Block? looseConnectionTargetBlock;
        private ConnectionSide sourceConnectionSide;
        private void ConnectorNode_DragStarted(object sender, DragStartedEventArgs e)
        {
            ConnectorNodeControl sourceConnectionNode = (ConnectorNodeControl)sender;
            sourceConnectionSide = sourceConnectionNode.ConnectionSide;

            Point startingPosition = UIHelperFunctions.GetFrameworkElementCenter(sourceConnectionNode);
            looseConnectionSourceBlock = Block;

            drawingLooseConnection = new LooseConnection(startingPosition);

            if (sourceConnectionNode.Connection is Connection existingConnection
                && existingConnection.LeftBlock is Block left
                && existingConnection.RightBlock is Block right)
            {
                left.RemoveConnectionTo(right);
                return;
            }

            Block.BlockDiagram!.BlockDiagramItems.Add(drawingLooseConnection);
        }

        private void ConnectorNode_DragDelta(object sender, DragDeltaEventArgs e)
        {
            ConnectorNodeControl? targetConnectorNode = FindConnectorNodeUnderCursor();
            looseConnectionTargetBlock = UIHelperFunctions.FindAncestor<BlockControl>(targetConnectorNode)?.Block;

            if (drawingLooseConnection is null)
                return;

            if (targetConnectorNode is not null
                && targetConnectorNode.Connection is null
                && looseConnectionSourceBlock is not null
                && looseConnectionTargetBlock is not null
                && targetConnectorNode.ConnectionSide != sourceConnectionSide
                && !looseConnectionTargetBlock.IsConnectedTo(looseConnectionSourceBlock))
            {
                drawingLooseConnection.End = UIHelperFunctions.GetFrameworkElementCenter(targetConnectorNode);
                return;
            }

            drawingLooseConnection.End = new Point(
                drawingLooseConnection.Start.X + e.HorizontalChange,
                drawingLooseConnection.Start.Y + e.VerticalChange);
        }

        private void ConnectorNode_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (drawingLooseConnection is not null)
                Block.BlockDiagram!.BlockDiagramItems.Remove(drawingLooseConnection);

            drawingLooseConnection = null;

            ConnectorNodeControl? targetConnectionNode = FindConnectorNodeUnderCursor();
            if (targetConnectionNode is null || targetConnectionNode.Connection is not null) return;
            looseConnectionTargetBlock = UIHelperFunctions.FindAncestor<BlockControl>(targetConnectionNode)?.Block;

            if (looseConnectionTargetBlock is null
                || looseConnectionSourceBlock is null
                || looseConnectionSourceBlock == looseConnectionTargetBlock
                || sourceConnectionSide == targetConnectionNode.ConnectionSide
                || looseConnectionSourceBlock.IsConnectedTo(looseConnectionTargetBlock)) return;

            if (sourceConnectionSide == ConnectionSide.Left)
                (looseConnectionSourceBlock, looseConnectionTargetBlock) = (looseConnectionTargetBlock, looseConnectionSourceBlock);

            looseConnectionSourceBlock.ConnectTo(looseConnectionTargetBlock);

            looseConnectionSourceBlock = null;
            looseConnectionTargetBlock = null;
        }
        #endregion
        #endregion
        #endregion

        private ConnectorNodeControl? FindConnectorNodeUnderCursor()
        {
            Canvas? activeCanvas = UIHelperFunctions.FindAncestor<Canvas>(this);
            if (activeCanvas is null) return null;
            Point currentMousePosition = Mouse.GetPosition(activeCanvas);
            ConnectorNodeControl? firstConnectorNodeUnderCursor = null;
            VisualTreeHelper.HitTest(
                activeCanvas,
                new HitTestFilterCallback(target =>
                {
                    if (target is not ConnectorNodeControl connector)
                        return HitTestFilterBehavior.Continue;

                    firstConnectorNodeUnderCursor = connector;

                    return HitTestFilterBehavior.Stop;
                }),
                new HitTestResultCallback(result => HitTestResultBehavior.Stop),
                new PointHitTestParameters(currentMousePosition));

            return firstConnectorNodeUnderCursor;
        }

        internal ConnectorNodeControl? FindConnectorNodeFromConnection(Connection connectionToFind)
        {
            return UIHelperFunctions.FindVisualChildren<ConnectorNodeControl>(this)
                .FirstOrDefault(connectionNode => connectionNode.Connection == connectionToFind);
        }

        public void Receive(BlockControlSelectionMessage message)
        {
            IsSelected = message.NewSelection == this;
        }
        #endregion
    }
}
