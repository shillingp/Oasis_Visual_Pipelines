using System.Collections.ObjectModel;
using Oasis_Pipelines.Interfaces;
using Oasis_Pipelines.Model;

namespace Oasis_Pipelines.Services.ConnectionManagement;

public sealed class ConnectionManager : IConnectionManager
{
    /// <inheritdoc />
    public ICollection<IConnection> AllConnections { get; } = new ObservableCollection<IConnection>();

    public void AddConnection(LooseConnection connection) => AllConnections.Add(connection);

    public Connection AddConnection(Block leftBlock, Block rightBlock)
    {
        Connection newConnection = new Connection(leftBlock, rightBlock);
        leftBlock.DownstreamConnections.Add(newConnection);
        rightBlock.UpstreamConnections.Add(newConnection);
        AllConnections.Add(newConnection);
        return newConnection;
    }

    public bool RemoveConnection(IConnection connection)
    {
        connection.Disconnect();
        return AllConnections.Remove(connection);
    }
}