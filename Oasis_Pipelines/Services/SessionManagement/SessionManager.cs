using Microsoft.Extensions.DependencyInjection;

namespace Oasis_Pipelines.Services.SessionManagement;

public class SessionManager : ISessionManager
{
    private readonly ISessionContextFactory _sessionContextFactory;

    public ICollection<ISessionContext> ActiveSessions { get; set; } = [];

    public SessionManager(ISessionContextFactory sessionContextFactory)
    {
        _sessionContextFactory = sessionContextFactory;
    }

    public ISessionContext CreateContext()
    {
        ISessionContext newSession =  _sessionContextFactory
            .WithSessionTitle("Default Session")
            .Create();

        ActiveSessions.Add(newSession);

        return newSession;
    }

    public bool CloseContext(ISessionContext sessionContext)
    {
        return ActiveSessions.Remove(sessionContext);
    }
}