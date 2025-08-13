using Oasis_Pipelines.Services.BlockManagement;
using Oasis_Pipelines.Services.ConnectionManagement;

namespace Oasis_Pipelines.Services.SessionManagement;

public class SessionContextFactory : ISessionContextFactory
{
    private readonly IConnectionManager _connectionManager;
    private readonly IBlockManager _blockManager;
    private string? _sessionTitle;

    public SessionContextFactory(
        IConnectionManager connectionManager,
        IBlockManager blockManager)
    {
        _connectionManager = connectionManager;
        _blockManager = blockManager;
    }

    public ISessionContextFactory WithSessionTitle(string sessionTitle)
    {
        _sessionTitle = sessionTitle;
        return this;
    }

    public ISessionContext Create()
    {
        ArgumentNullException.ThrowIfNull(_sessionTitle);

        return new SessionContext(_connectionManager, _blockManager, _sessionTitle);
    }
}