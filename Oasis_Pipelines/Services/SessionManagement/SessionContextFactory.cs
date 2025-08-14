using Oasis_Pipelines.Services.BlockCalculation;
using Oasis_Pipelines.Services.BlockManagement;
using Oasis_Pipelines.Services.ConnectionManagement;

namespace Oasis_Pipelines.Services.SessionManagement;

public class SessionContextFactory : ISessionContextFactory
{
    private readonly IConnectionManager _connectionManager;
    private readonly IBlockManager _blockManager;
    private readonly IBlockCalculation _blockCalculation;
    private string? _sessionTitle;

    public SessionContextFactory(
        IConnectionManager connectionManager,
        IBlockManager blockManager,
        IBlockCalculation blockCalculation)
    {
        _connectionManager = connectionManager;
        _blockManager = blockManager;
        _blockCalculation = blockCalculation;
    }

    public ISessionContextFactory WithSessionTitle(string sessionTitle)
    {
        _sessionTitle = sessionTitle;
        return this;
    }

    public ISessionContext Create()
    {
        ArgumentNullException.ThrowIfNull(_sessionTitle);

        return new SessionContext(_connectionManager, _blockManager, _blockCalculation, _sessionTitle);
    }
}