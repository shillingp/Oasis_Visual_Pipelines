using System.Collections.Concurrent;
using System.Windows;
using Oasis_Pipelines.Controls.Wpf.Interfaces;
using Oasis_Pipelines.Model;
using Oasis_Pipelines.Operations.Enums;
using Oasis_Pipelines.Shared.Extensions;
using Oasis_Pipelines.Shared.Wpf.Extensions;

namespace Oasis_Pipelines.Controls.Wpf.Services;

public class ConnectorVisualRegistry : IConnectorVisualRegistry
{
    private readonly ISet<ConnectorNode> _connectorNodes = new HashSet<ConnectorNode>();


    public void Register(ConnectorNode connectorNode) => _connectorNodes.Add(connectorNode);

    public void Unregister(ConnectorNode connectorNode) => _connectorNodes.Remove(connectorNode);

    public bool TryGetConnectorNode(Connection? connection, ConnectionSide side, out ConnectorNode? connectorNode)
    {
        connectorNode = _connectorNodes.FirstOrDefault(keyValuePair =>
            keyValuePair.Connection == connection && keyValuePair.ConnectionSide == side);
        return connectorNode != null;
    }

    public bool TryGetConnectorNodeAtMousePosition(out ConnectorNode? connectorNode)
    {
        connectorNode = _connectorNodes
            .FirstOrDefault(UiExtensions.CursorOverlapsWithFrameworkElement);
        return connectorNode != null;
    }
}