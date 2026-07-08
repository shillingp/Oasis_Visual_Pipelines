using System.Collections.ObjectModel;
using Oasis_Pipelines.Interfaces;
using Oasis_Pipelines.Model;

namespace Oasis_Pipelines.Services.ConnectionManagement;

public sealed class ConnectionManager : IConnectionManager
{
    /// <inheritdoc />
    public ICollection<IConnection> AllConnections { get; } = new ObservableCollection<IConnection>();

    public void AddConnection(IConnection connection) => AllConnections.Add(connection);

    public bool RemoveConnection(IConnection connection)
    {
        connection.Disconnect();
        return AllConnections.Remove(connection);
    }
}