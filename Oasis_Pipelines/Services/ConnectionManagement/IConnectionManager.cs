using Oasis_Pipelines.Model;

namespace Oasis_Pipelines.Services.ConnectionManagement;

public interface IConnectionManager
{
    /// <summary>
    /// A collection containing all active <see cref="Connection"/> objects
    /// </summary>
    ICollection<IConnection> AllConnections { get; }

    Connection AddConnection(Block leftSide, Block rightSide);

    void AddConnection(LooseConnection looseConnection);

    bool RemoveConnection(IConnection connection);
}