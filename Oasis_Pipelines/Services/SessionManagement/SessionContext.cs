using Oasis_Pipelines.Services.BlockCalculation;
using Oasis_Pipelines.Services.BlockManagement;
using Oasis_Pipelines.Services.ConnectionManagement;

namespace Oasis_Pipelines.Services.SessionManagement;

public class SessionContext : ISessionContext
{
    /// <inheritdoc />
    public string SessionTitle { get; set; }

    public IConnectionManager ConnectionManager { get; }
    public IBlockManager BlockManager { get; }
    public IBlockCalculation BlockCalculation { get; }

    public SessionContext(
        IConnectionManager connectionManager,
        IBlockManager blockManager,
        IBlockCalculation blockCalculation,
        string sessionTitle)
    {
        ConnectionManager = connectionManager;
        BlockManager = blockManager;
        BlockCalculation = blockCalculation;
        SessionTitle = sessionTitle;
    }
}