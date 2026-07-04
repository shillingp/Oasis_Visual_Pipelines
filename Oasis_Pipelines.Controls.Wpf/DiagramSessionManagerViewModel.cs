using Oasis_Pipelines.Services.SessionManagement;
using PropertyChanged;

namespace Oasis_Pipelines.Controls.Wpf;

[AddINotifyPropertyChangedInterface]
public class DiagramSessionManagerViewModel
{
    public ISessionManager? SessionManager { get; set; }

    public DiagramSessionManagerViewModel(ISessionManager sessionManager)
    {
        SessionManager = sessionManager;
    }
}