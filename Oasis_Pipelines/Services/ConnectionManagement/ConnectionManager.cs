using System.Collections.ObjectModel;
using Oasis_Pipelines.Model;

namespace Oasis_Pipelines.Services.ConnectionManagement;

public sealed class ConnectionManager : IConnectionManager
{
    /// <inheritdoc />
    public ICollection<IConnection> AllConnections { get; } = new ObservableCollection<IConnection>();

    public Connection AddConnection(Block leftSide, Block rightSide)
    {
        Connection newConnection = new Connection(leftSide, rightSide);

        leftSide.DownstreamConnections.Add(newConnection);
        rightSide.UpstreamConnections.Add(newConnection);

        AllConnections.Add(newConnection);
        return newConnection;
    }

    public void AddConnection(LooseConnection looseConnection)
    {
        AllConnections.Add(looseConnection);
    }

    public bool RemoveConnection(Connection connection)
    {
        throw new NotImplementedException();
    }

    public bool RemoveConnection(IConnection connection)
    {
        if (connection is Connection realConnection)
        {
            realConnection.LeftBlock.DownstreamConnections.Remove(realConnection);
            realConnection.RightBlock.UpstreamConnections.Remove(realConnection);
        }

        return AllConnections.Remove(connection);
    }
}