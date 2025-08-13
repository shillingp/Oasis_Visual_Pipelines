using Oasis_Pipelines.Model;

namespace Oasis_Pipelines.Services.ConnectionManagement;

public interface IConnectionManager
{
    /// <summary>
    /// A collection containing all active <see cref="Connection"/> objects
    /// </summary>
    ICollection<Connection> AllConnections { get; set; }

    Connection AddConnection(Block leftSide, Block rightSide);

    bool RemoveConnection(Connection connection);
}