using Oasis_Pipelines.Model;

namespace Oasis_Pipelines.Services;

public class BlockConnectionService : IBlockConnectionService
{
    public Connection AddConnection(Block leftSide, Block rightSide)
    {
        Connection newConnection = new Connection(leftSide, rightSide);

        leftSide.DownstreamConnections.Add(newConnection);
        rightSide.UpstreamConnections.Add(newConnection);

        return newConnection;
    }

    public void RemoveConnection(Connection connection)
    {
        connection.LeftBlock.DownstreamConnections.Remove(connection);
        connection.RightBlock.UpstreamConnections.Remove(connection);
    }
}