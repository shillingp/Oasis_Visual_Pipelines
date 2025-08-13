using Oasis_Pipelines.Services.SessionManagement;

namespace Oasis_Pipelines.Diagrams;

public class DiagramSessionViewModel
{
    public ISessionManager SessionManager { get; }

    public DiagramSessionViewModel(ISessionManager sessionManager)
    {
        SessionManager = sessionManager;

        SessionManager.CreateContext();
    }
}