using Oasis_Pipelines.Model;

namespace Oasis_Pipelines.Services.ConnectionManagement;

public sealed class ConnectionManager : IConnectionManager
{
    /// <inheritdoc />
    public ICollection<Connection> AllConnections { get; set; } = [];

    public Connection AddConnection(Block leftSide, Block rightSide)
    {
        Connection newConnection = new Connection(leftSide, rightSide);

        leftSide.DownstreamConnections.Add(newConnection);
        rightSide.UpstreamConnections.Add(newConnection);

        AllConnections.Add(newConnection);
        return newConnection;
    }

    public bool RemoveConnection(Connection connection)
    {
        connection.LeftBlock.DownstreamConnections.Remove(connection);
        connection.RightBlock.UpstreamConnections.Remove(connection);

        return AllConnections.Remove(connection);
    }
}