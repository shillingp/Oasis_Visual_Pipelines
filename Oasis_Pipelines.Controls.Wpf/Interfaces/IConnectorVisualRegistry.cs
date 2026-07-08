using Oasis_Pipelines.Model;
using Oasis_Pipelines.Operations.Enums;

namespace Oasis_Pipelines.Controls.Wpf.Interfaces;

public interface IConnectorVisualRegistry
{
    void Register(ConnectorNode connectorNode);
    void Unregister(ConnectorNode connectorNode);

    bool TryGetConnectorNode(
        Connection? connection,
        ConnectionSide side,
        out ConnectorNode? connectorNode);

    bool TryGetConnectorNodeAtMousePosition(out ConnectorNode? connectorNode);
}