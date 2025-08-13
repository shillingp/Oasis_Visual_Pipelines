using Oasis_Pipelines.Services.BlockManagement;
using Oasis_Pipelines.Services.ConnectionManagement;

namespace Oasis_Pipelines.Services.SessionManagement;

public class SessionContext : ISessionContext
{
    /// <inheritdoc />
    public string SessionTitle { get; set; }

    public IConnectionManager ConnectionManager { get; }
    public IBlockManager BlockManager { get; }

    public SessionContext(
        IConnectionManager connectionManager,
        IBlockManager blockManager,
        string sessionTitle)
    {
        ConnectionManager = connectionManager;
        BlockManager = blockManager;
        SessionTitle = sessionTitle;
    }
}