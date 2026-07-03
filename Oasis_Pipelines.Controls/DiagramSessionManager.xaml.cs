using System.Windows;
using System.Windows.Controls;
using Oasis_Pipelines.Services.SessionManagement;

namespace Oasis_Pipelines.Controls;

/// <summary>
/// Interaction logic for UserControl1.xaml
/// </summary>
public partial class DiagramSessionManager : UserControl
{
    public ISessionManager SessionManager
    {
        get { return (ISessionManager)GetValue(SessionManagerProperty); }
        set { SetValue(SessionManagerProperty, value); }
    }

    public static readonly DependencyProperty SessionManagerProperty =
        DependencyProperty.Register(
            nameof(SessionManager),
            typeof(ISessionManager),
            typeof(DiagramSessionManager),
            new FrameworkPropertyMetadata(null,
                (d, e) => (d as DiagramSessionManager)?.SessionManager = (ISessionManager)e.NewValue));

    public DiagramSessionManager()
    {
        InitializeComponent();
    }
}