namespace Oasis_Pipelines.Services.SessionManagement;

public interface ISessionContextFactory
{
    ISessionContextFactory WithSessionTitle(string sessionTitle);

    ISessionContext Create();
}