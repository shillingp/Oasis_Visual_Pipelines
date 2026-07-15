using System.Drawing;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Oasis_Pipelines.Controls.Wpf.Interfaces;
using Oasis_Pipelines.Interfaces;
using Oasis_Pipelines.Model;
using Oasis_Pipelines.Operations.Enums;
using Oasis_Pipelines.Services.ConnectionManagement;
using Oasis_Pipelines.Services.SessionManagement;
using Oasis_Pipelines.Shared.Wpf.Extensions;
using Oasis_Pipelines.Shared.Wpf.Interfaces;
using Oasis_Pipelines.Shared.Wpf.Interfaces.Dragging;
using Point = System.Windows.Point;

namespace Oasis_Pipelines.Controls.Wpf.Services;

public class ConnectorDragController : IConnectionDragController
{
    private readonly ISessionManager _sessionManager;
    private readonly IConnectorVisualRegistry _connectorVisualRegistry;

    private Connection? _existingConnection;
    private ConnectorNode? _startingNode;
    private ConnectorNode? _targetConnectorNode;
    private LooseConnection? _drawingLooseConnection;

    public ConnectorDragController(
        ISessionManager sessionManager,
        IConnectorVisualRegistry connectorVisualRegistry)
    {
        _sessionManager = sessionManager;
        _connectorVisualRegistry = connectorVisualRegistry;
    }

    public void StartDrag(
        PointF startingPosition,
        ConnectionSide dragSide)
    {
        _connectorVisualRegistry.TryGetConnectorNodeAtMousePosition(out _startingNode);

        _drawingLooseConnection = new LooseConnection(startingPosition);
        _sessionManager.CurrentSession?.ConnectionManager.AddConnection(_drawingLooseConnection);
    }

    public void StartDrag(
        Connection connector,
        ConnectionSide dragSide,
        PointF mousePosition)
    {
        _sessionManager.CurrentSession?.ConnectionManager.RemoveConnection(connector);
    }

    public void UpdateDrag(PointF mousePosition)
    {
        if (_drawingLooseConnection is null) return;

        bool mouseIsHoveringOverConnectorNode =
            _connectorVisualRegistry.TryGetConnectorNodeAtMousePosition(out _targetConnectorNode);
        if (mouseIsHoveringOverConnectorNode
            && TryGetValidConnectionEndpoints() is not null)
        {
            _drawingLooseConnection.EndPosition =
                _targetConnectorNode!.GetFrameworkElementCenter() - _drawingLooseConnection.Position.ToSizeF();
            return;
        }

        _drawingLooseConnection.EndPosition = mousePosition;
    }

    public void StopDrag(PointF mousePosition)
    {
        if (_drawingLooseConnection is null) return;

        // if (_startingNode?.Block is { } startBlock
        //     && _targetConnectorNode?.Block is { } targetBlock
        //     && startBlock != targetBlock)
        if (_startingNode is not null
            && _targetConnectorNode is not null
            && TryGetValidConnectionEndpoints() is { Start: { } startBlock, Target: { } targetBlock })
        {
            Connection? newConnection =
                _sessionManager.CurrentSession?.ConnectionManager.AddConnection(startBlock, targetBlock);
            _startingNode.Connection = newConnection;
            _targetConnectorNode.Connection = newConnection;
        }

        _sessionManager.CurrentSession?.ConnectionManager.RemoveConnection(_drawingLooseConnection);
    }

    private (Block Start, Block Target)? TryGetValidConnectionEndpoints()
    {
        if (_startingNode?.Block is not { } startBlock) return null;
        if (_targetConnectorNode?.Block is not { } targetBlock) return null;
        if (startBlock == targetBlock) return null;
        if (AreBlocksAlreadyConnected(targetBlock, startBlock)) return null;

        return (startBlock, targetBlock);
    }

    private static bool AreBlocksAlreadyConnected(Block block, Block otherBlock) =>
        block.DownstreamConnections
            .Concat(block.UpstreamConnections)
            .Any(connection => connection.LeftBlock == otherBlock
                               || connection.RightBlock == otherBlock);
}


// private LooseConnection? drawingLooseConnection;
//     private Block? looseConnectionSourceBlock;
//     private Block? looseConnectionTargetBlock;
//     private ConnectionSide sourceConnectionSide;
//     private void ConnectorNode_DragStarted(object sender, DragStartedEventArgs e)
//     {
//         ConnectorNodeControl sourceConnectionNode = (ConnectorNodeControl)sender;
//         sourceConnectionSide = sourceConnectionNode.ConnectionSide;
//
//         Point startingPosition = UIHelperFunctions.GetFrameworkElementCenter(sourceConnectionNode);
//         looseConnectionSourceBlock = Block;
//
//         drawingLooseConnection = new LooseConnection(startingPosition);
//
//         if (sourceConnectionNode.Connection is Connection existingConnection
//             && existingConnection.LeftBlock is Block left
//             && existingConnection.RightBlock is Block right)
//         {
//             left.RemoveConnectionTo(right);
//             return;
//         }
//
//         Block.BlockDiagram!.BlockDiagramItems.Add(drawingLooseConnection);
//     }
//
//     private void ConnectorNode_DragDelta(object sender, DragDeltaEventArgs e)
//     {
//         ConnectorNodeControl? targetConnectorNode = FindConnectorNodeUnderCursor();
//         looseConnectionTargetBlock = UIHelperFunctions.FindAncestor<BlockControl>(targetConnectorNode)?.Block;
//
//         if (drawingLooseConnection is null)
//             return;
//
//         if (targetConnectorNode is not null
//             && targetConnectorNode.Connection is null
//             && looseConnectionSourceBlock is not null
//             && looseConnectionTargetBlock is not null
//             && targetConnectorNode.ConnectionSide != sourceConnectionSide
//             && !looseConnectionTargetBlock.IsConnectedTo(looseConnectionSourceBlock))
//         {
//             drawingLooseConnection.End = UIHelperFunctions.GetFrameworkElementCenter(targetConnectorNode);
//             return;
//         }
//
//         drawingLooseConnection.End = new Point(
//             drawingLooseConnection.Start.X + e.HorizontalChange,
//             drawingLooseConnection.Start.Y + e.VerticalChange);
//     }
//
//     private void ConnectorNode_DragCompleted(object sender, DragCompletedEventArgs e)
//     {
//         if (drawingLooseConnection is not null)
//             Block.BlockDiagram!.BlockDiagramItems.Remove(drawingLooseConnection);
//
//         drawingLooseConnection = null;
//
//         ConnectorNodeControl? targetConnectionNode = FindConnectorNodeUnderCursor();
//         if (targetConnectionNode is null || targetConnectionNode.Connection is not null) return;
//         looseConnectionTargetBlock = UIHelperFunctions.FindAncestor<BlockControl>(targetConnectionNode)?.Block;
//
//         if (looseConnectionTargetBlock is null
//             || looseConnectionSourceBlock is null
//             || looseConnectionSourceBlock == looseConnectionTargetBlock
//             || sourceConnectionSide == targetConnectionNode.ConnectionSide
//             || looseConnectionSourceBlock.IsConnectedTo(looseConnectionTargetBlock)) return;
//
//         if (sourceConnectionSide == ConnectionSide.Left)
//             (looseConnectionSourceBlock, looseConnectionTargetBlock) = (looseConnectionTargetBlock, looseConnectionSourceBlock);
//
//         looseConnectionSourceBlock.ConnectTo(looseConnectionTargetBlock);
//
//         looseConnectionSourceBlock = null;
//         looseConnectionTargetBlock = null;
//     }