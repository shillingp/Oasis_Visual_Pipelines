using Oasis_Pipelines.Model;
using Oasis_Pipelines.Operations.Enums;

namespace Oasis_Pipelines.Controls.Wpf.Interfaces;

public interface IConnectorVisualRegistry
{
    void Register(Connection connection, ConnectionSide side, ConnectorNode connectorNode);
    void Unregister(Connection connection, ConnectionSide side, ConnectorNode connectorNode);

    bool TryGetConnectorNode(
        Connection connection,
        ConnectionSide side,
        out ConnectorNode? connectorNode);
}