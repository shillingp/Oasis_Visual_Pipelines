using Oasis_Pipelines.Services.SessionManagement;
using PropertyChanged;

namespace Oasis_Pipelines.Controls;

[AddINotifyPropertyChangedInterface]
public class DiagramSessionManagerViewModel
{
    public ISessionManager? SessionManager { get; set; }

    public DiagramSessionManagerViewModel(ISessionManager sessionManager)
    {
        SessionManager = sessionManager;
    }
}