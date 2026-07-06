using Oasis_Pipelines.Interfaces;
using Oasis_Pipelines.Model;

namespace Oasis_Pipelines.Services.ConnectionManagement;

public interface IConnectionManager
{
    /// <summary>
    /// A collection containing all active <see cref="Connection"/> objects
    /// </summary>
    ICollection<IConnection> AllConnections { get; }

    void AddConnection(IConnection looseConnection);

    bool RemoveConnection(IConnection connection);
}