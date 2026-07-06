using System.Drawing;
using System.Windows;
using System.Windows.Controls.Primitives;
using Oasis_Pipelines.Model;
using Oasis_Pipelines.Services.ConnectionManagement;
using Oasis_Pipelines.Services.SessionManagement;
using Oasis_Pipelines.Shared.Wpf.Interfaces;

namespace Oasis_Pipelines.Controls.Wpf.Services;

public class ConnectorDragController : IDragController
{
    private readonly IConnectionManager _connectionManager;
    private readonly ISessionManager _sessionManager;

    private LooseConnection? _drawingLooseConnection;

    public ConnectorDragController(
        IConnectionManager connectionManager,
        ISessionManager sessionManager)
    {
        _connectionManager = connectionManager;
        _sessionManager = sessionManager;
    }

    public void StartDrag(PointF startingPosition, DragStartedEventArgs dragStartedEventArgs)
    {
        _drawingLooseConnection = new LooseConnection(startingPosition);
        _sessionManager.CurrentSession?.ConnectionManager.AddConnection(_drawingLooseConnection);
    }

    public void UpdateDrag(DragDeltaEventArgs dragDeltaEventArgs)
    {
        if (_drawingLooseConnection is null) return;

    }

    public void StopDrag(DragCompletedEventArgs dragCompletedEventArgs)
    {
        if (_drawingLooseConnection is null) return;
        _sessionManager.CurrentSession?.ConnectionManager.RemoveConnection(_drawingLooseConnection);
    }
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