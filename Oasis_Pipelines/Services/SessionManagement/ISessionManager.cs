namespace Oasis_Pipelines.Services.SessionManagement;

public interface ISessionManager
{
    ICollection<ISessionContext> ActiveSessions { get; set; }

    ISessionContext CreateContext();

    bool CloseContext(ISessionContext sessionContext);
}