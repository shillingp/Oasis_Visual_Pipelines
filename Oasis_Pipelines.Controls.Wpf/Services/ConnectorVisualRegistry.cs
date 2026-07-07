using Oasis_Pipelines.Controls.Wpf.Interfaces;
using Oasis_Pipelines.Model;
using Oasis_Pipelines.Operations.Enums;

namespace Oasis_Pipelines.Controls.Wpf.Services;

public class ConnectorVisualRegistry : IConnectorVisualRegistry
{
    private readonly IDictionary<(Connection, ConnectionSide), ConnectorNode> _connectorNodes =
        new Dictionary<(Connection, ConnectionSide), ConnectorNode>();

    public void Register(Connection connection, ConnectionSide side, ConnectorNode connectorNode) =>
        _connectorNodes.Add((connection, side), connectorNode);

    public void Unregister(Connection connection, ConnectionSide side, ConnectorNode connectorNode) =>
        _connectorNodes.Remove((connection, side));

    public bool TryGetConnectorNode(Connection connection, ConnectionSide side, out ConnectorNode? connectorNode) =>
        _connectorNodes.TryGetValue((connection, side), out connectorNode);
}